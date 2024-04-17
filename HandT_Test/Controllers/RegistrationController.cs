using HandT_Test.Authentication;
using HandT_Test.Controllers;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_PG.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HandT_Test_Mysql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogger<RegistrationController> logger
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        [Route("register-participator")]
        public async Task<IActionResult> RegisterParticipator([FromBody] UserRegistrationRequest quickRegistration)
        {
            var userNameExists = await _userManager.FindByNameAsync(quickRegistration.Username);

            var userEmailExists = await _userManager.FindByEmailAsync(quickRegistration.Email);

            if (userNameExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new DomainEntities.ApiResponse { ResponseCode = 500, ResponseMessage = "User already exists!" });
            }

            if (userEmailExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new DomainEntities.ApiResponse { ResponseCode = 500, ResponseMessage = "User Email already exists!" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = quickRegistration.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = quickRegistration.Username,
                // VerificationToken = CreateRandomToken()
            };

            var result = await _userManager.CreateAsync(user, quickRegistration.Password);
            Console.WriteLine(result);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   new DomainEntities.ApiResponse
                                   {
                                       Msg = "Error",
                                       ResponseMessage = "User creation failed! Please check user details and try again."
                                   });
            }

            await _userManager.AddToRoleAsync(user, UserRoles.Participator);

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                responseMessage = "User created successfully!",
                username = user.UserName
            });

            //return Ok(new ApiResponse { Msg = "Success", ResponseMessage = "User created successfully!" });

        }

        [HttpPost]
        [Route("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginModel model)
        {

            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddDays(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });

                }

            }
            catch (Exception ex)
            {

            }
            return Unauthorized();
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

    }
}

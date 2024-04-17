using HandT_Test.Authentication;
using HandT_Test.Controllers;
using HandT_Test.DbContext;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_PG.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HandT_Test_Mysql.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private DatabaseContext _databaseContext;

        public AdminController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogger<AuthController> logger,
            IHttpContextAccessor httpContextAccessor,
            DatabaseContext dbContext
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _databaseContext = dbContext;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Admin-Register")]
        public async Task<IActionResult> AdminRegister([FromBody] AdminRegister adminRegister)
        {
            var userExists = await _userManager.FindByNameAsync(adminRegister.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Status = "Error", Message = "User already exists!" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = adminRegister.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = adminRegister.Username
            };

            var result = await _userManager.CreateAsync(user, adminRegister.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse
                    {
                        Status = "Error",
                        Message = "Admin user creation failed! Please check user details and try again."
                    });


            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            return Ok(new ApiResponse { Status = "Success", Message = "Admin User created successfully!" });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Admin-Login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginModel model)
        {
            bool IsAdminUser = false;
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("Admin"))
                {
                    IsAdminUser = true;
                }

                if (IsAdminUser == false)
                {
                    return Unauthorized();
                }
                else
                {

                    var authClaims = new List<Claim>
                {
                    new Claim("IsAdmin", "YES"),
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
            return Unauthorized();
        }


        //
        [HttpGet]
        [Route("Admin-Test")]
        public async Task<IActionResult> AdminTest()
        {
            return Ok("Admin Test");
        }

    }
}

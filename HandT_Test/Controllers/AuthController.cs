using Azure;
using HandT_Test.Authentication;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_PG.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HandT_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        private readonly IWebHostEnvironment _environment;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ILogger<AuthController> logger, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
            _environment = environment;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model,IFormFile image)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Status = "Error", Message = "User already exists!" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Username,
                Email = model.Email,
                isIndivCompany = model.isIndivCompany,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                DOB = model.DOB,
                Gender = model.Gender,
                Occupation = model.Occupation,
                Website = model.Website,
                Address = model.Address,
                Interest = model.Interest,
                Bio = model.Bio,
                //Picture = model.Picture,
                SecurityStamp = Guid.NewGuid().ToString()
            };


            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                                    , new ApiResponse
                                    {
                                        Status = "Error",
                                        Message = "User creation failed! Please check user details and try again."
                                    });

            }
            if (image != null && image.Length > 0)
            {
                var fileName = model.Username + Path.GetExtension(image.FileName);
                var filePath = Path.Combine("wwwroot", "Upload", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                user.Picture = fileName;

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
                    {
                        Status = "Error",
                        Message = "Failed to update user with picture information."
                    });
                }
            }


            return Ok(new ApiResponse { Status = "Success", Message = "User created successfully!" });
        }

        //generate token
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            _logger.LogInformation("Inside Login.." + model.Username);
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
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

               
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });

            }
            return Unauthorized();
        }

        //register admin
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse
                    {
                        Status = "Error",
                        Message = "User creation failed! Please check user details and try again."
                    });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new ApiResponse { Status = "Success", Message = "User created successfully!" });

        }



        [HttpPost]
        [Route("Individual-Profile")]
        public async Task<IActionResult> IndividualProfile([FromForm] RegisterModel model, IFormFile? image)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Status = "Error", Message = "User already exists!" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Username,
                Email = model.Email,
                isIndivCompany = model.isIndivCompany,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                DOB = model.DOB,
                Gender = model.Gender,
                Occupation = model.Occupation,
                Website = model.Website,
                Address = model.Address,
                Interest = model.Interest,
                Bio = model.Bio,
                //Picture = model.Picture,
                SecurityStamp = Guid.NewGuid().ToString()
            };


            var result = await _userManager.CreateAsync(user, model.Password);
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

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                                    , new ApiResponse
                                    {
                                        Status = "Error",
                                        Message = "User creation failed! Please check user details and try again."
                                    });

            }
            if (image != null && image.Length > 0)
            {
                var fileName = model.Username + Path.GetExtension(image.FileName);
                var filePath = Path.Combine("wwwroot", "Upload", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                user.Picture = fileName;

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
                    {
                        Status = "Error",
                        Message = "Failed to update user with picture information."
                    });
                }
            }


            //return Ok(new ApiResponse { Status = "Success", Message = "User created successfully!" });
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                responseMessage = "User created successfully!",
                username = user.UserName
            });
        }

        [HttpPost]
        [Route("Corporate-Profile")]
        public async Task<IActionResult> CorporateProfile([FromForm] RegisterModel model, IFormFile? image)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Status = "Error", Message = "User already exists!" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Username,
                Email = model.Email,
                isIndivCompany = model.isIndivCompany,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                DOB = model.DOB,
                Gender = model.Gender,
                Occupation = model.Occupation,
                Website = model.Website,
                Address = model.Address,
                Interest = model.Interest,
                Bio = model.Bio,
                //Picture = model.Picture,
                SecurityStamp = Guid.NewGuid().ToString()
            };


            var result = await _userManager.CreateAsync(user, model.Password);
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

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                                    , new ApiResponse
                                    {
                                        Status = "Error",
                                        Message = "User creation failed! Please check user details and try again."
                                    });

            }
            if (image != null && image.Length > 0)
            {
                var fileName = model.Username + Path.GetExtension(image.FileName);
                var filePath = Path.Combine("wwwroot", "Upload", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                user.Picture = fileName;

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
                    {
                        Status = "Error",
                        Message = "Failed to update user with picture information."
                    });
                }
            }


            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                responseMessage = "User created successfully!",
                username = user.UserName
            });
        }


    }
}

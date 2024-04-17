using HandT_Test.Authentication;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_PG.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace HandT_Test.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TestController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("Hello")]
        public async Task<IActionResult> Test()
        {
            return Ok("Hello World..");
        }

        [HttpPost]
        [Route("add-role")]
        public async Task<IActionResult> AddRole([FromBody] RoleModel roleModel)
        {
            if (roleModel.RoleName != null || !roleModel.RoleName.IsNullOrEmpty())
            {
                if (!await _roleManager.RoleExistsAsync(roleModel.RoleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleModel.RoleName));
                    return Ok(new ApiResponse
                    {
                        Status = "Success",
                        Message = "New Role created successfully!"
                    });
                }
            }
            return Unauthorized();
        }

        //add user to a role
        [HttpPost]
        [Route("add-user-to-role")]
        public async Task<IActionResult> AddUserToRole(string UserName, string Role)
        {
            var user = await _userManager.FindByNameAsync(UserName);

            if(user != null)
            {
                var stat = await _userManager.AddToRoleAsync(user, Role);
            }
           

            return Ok(new ApiResponse { Status = "Success", Message = "User added to role successfully!" });
        }
    }
}

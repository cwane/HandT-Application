using HandT_Test.Authentication;
using HandT_Test_PG.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HandT_Test_PG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokentestController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public TokentestController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Publisher)]
        [HttpGet]
        [Route("publisher-only")]
        public async Task<IActionResult> CheckPublisher()
        {            
            ClaimsVM model = new ClaimsVM();
            var accessToken = Request.Headers["Authorization"];
            var tokeninfo = accessToken.ToString().Split(' ');
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(tokeninfo[1]);
            string userdetails = "";
            List<string> claims = new List<string>();
            foreach (var item in jwt.Claims)
            {
                userdetails += item.Value.ToString() + ", ";
                claims.Add(item.Value.ToString());
            }

            var un = claims[0];//get username

            var user_details = await _userManager.FindByNameAsync(un);
            var user_id = user_details.Id;

            return Ok("Check Publisher..");
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Approver)]
        [HttpGet]
        [Route("approver-only")]
        public async Task<IActionResult> CheckApprover()
        {
            return Ok("Check Approver..");
        }
    }

    public class ClaimsVM
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Isverified { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
    }

}

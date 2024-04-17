using HandT_Test.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HandT_Test.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Author)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAuthor()
        {
            return Ok("Author List Accessed...");
        }
    }
}

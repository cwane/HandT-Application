using HandT_Test.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HandT_Test_Mysql.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Creator)]
    [Route("api/[controller]")]
    [ApiController]
    public class EventscreatorController : ControllerBase
    {
        public EventscreatorController() { 
        }



    }
}

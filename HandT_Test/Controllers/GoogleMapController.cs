using HandT_Api_Layer.DomainInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandT_Api_Layer.Controllers
{
    [AllowAnonymous]
    //[Route("api/[controller]")]
    public class GoogleMapController : Controller
    {
        public IActionResult Index()
        {
            return View("~/GoogleMap/Index.cshtml");
        }
    }
}

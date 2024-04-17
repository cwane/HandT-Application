using Microsoft.AspNetCore.Mvc;

namespace HandT_Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

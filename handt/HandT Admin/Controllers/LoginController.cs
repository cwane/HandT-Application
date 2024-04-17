using HandT_Admin.DomainEntities;
using Microsoft.AspNetCore.Mvc;

namespace HandT_Admin.Controllers
{        
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserLogin userLogin)
        {
            return View();
        }


        public IActionResult Dashboard()
        {
            return View();
        }
    }
}

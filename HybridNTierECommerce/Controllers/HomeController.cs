using Microsoft.AspNetCore.Mvc;

namespace HybridNTierECommerce.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public IActionResult Login()
        {
            return View();
        }
    }
}

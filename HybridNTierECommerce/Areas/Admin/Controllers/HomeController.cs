using Microsoft.AspNetCore.Mvc;

namespace HybridNTierECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ShopFlower.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

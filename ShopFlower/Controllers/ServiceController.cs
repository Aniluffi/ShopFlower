using Microsoft.AspNetCore.Mvc;

namespace ShopFlower.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

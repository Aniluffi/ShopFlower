using Microsoft.AspNetCore.Mvc;

namespace ShopFlower.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

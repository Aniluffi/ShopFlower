using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopFlower.Controllers
{
    [Authorize]
    public class WishListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

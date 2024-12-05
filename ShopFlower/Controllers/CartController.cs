using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopFlower.Data;
using ShopFlower.IService.ServiceUser;
using ShopFlower.Data.Models;

namespace ShopFlower.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private IServiceUser _serviceUser;
        private DbContextOptions<ApplicationContext> _options;

        public CartController(IServiceUser serviceUser, DbContextOptions<ApplicationContext> options)
        {
            _serviceUser = serviceUser;
            _options = options;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var id = User.FindFirst("UserId")?.Value;
                var cards = await _serviceUser.GetProductInCart(Convert.ToInt32(id));
                return View(cards);
            }
            catch (Exception)
            {
                return View(new List<Cart>());
            }
        }

        public async Task<IActionResult> AddProductCart(int product)
        {
            var userId = User.FindFirst("UserId")?.Value;
            await _serviceUser.AddProductInCart(Convert.ToInt32(userId),product);
            return View("Index");
        }
    }
}

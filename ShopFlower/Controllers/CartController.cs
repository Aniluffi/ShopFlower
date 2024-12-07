using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopFlower.Data;
using ShopFlower.Data.Models;
using ShopFlower.IService.ServiceUser;

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
                var userId = User.FindFirst("UserId")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("Пользователь не авторизован.");
                }

                var cart = await _serviceUser.GetProductInCart(Convert.ToInt32(userId));
                return View(cart);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка в Index: {ex.Message}");
                return View(new Cart());
            }
        }

        public async Task<IActionResult> AddProductCart(int product)
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("Пользователь не авторизован.");
                }

                if (product <= 0)
                {
                    throw new ArgumentException("Неверный идентификатор продукта.");
                }

                var exceptions = await _serviceUser.AddProductInCart(Convert.ToInt32(userId), product);

                if (exceptions.Any())
                {
                    // Сообщение об ошибке пользователю
                    TempData["Errors"] = exceptions.Select(e => e.Message).ToList();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка в AddProductCart: {ex.Message}");
                TempData["Errors"] = new List<string> { "Произошла ошибка при добавлении товара в корзину." };
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

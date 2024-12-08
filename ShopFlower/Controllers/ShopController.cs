using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopFlower.Data;
using ShopFlower.Data.Models;
using ShopFlower.IService.ServiceProduct;
using ShopFlower.Models;
namespace ShopFlower.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        private DbContextOptions<ApplicationContext> _options;

        public ShopController(IProductService productService, DbContextOptions<ApplicationContext> options)
        {
            this._productService = productService;
            _options = options;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ListProduct products = new ListProduct
                {
                    Products = await _productService.GetProductShort(10, 0)
                };
                return View(products);
            }
            catch (Exception)
            {
                return View(new ListProduct());
            }
        }


        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _productService.GetProduct(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Filter([FromBody]ProductFilter filter)
        {
            try
            {
                if (filter == null)
                    return Json(new { success = false, message = "Фильтр не может быть пустым" });

                var productFilter = await _productService.GetProductByFilter(filter);

                if (productFilter == null || productFilter.Count == 0)
                    return Json(new { success = false, message = "Нет товаров по данному фильтру" });

                return Json(new { success = true, products = productFilter });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopFlower.Data;
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
                    Products = await _productService.GetProductShort(1, 0)
                };
                return View(products);
            }
            catch(Exception)
            {
                return View(new ListProduct());
            }
        }


        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _productService.GetProduct(id);
            return View(product);
        }


    }
}

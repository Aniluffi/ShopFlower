using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopFlower.Data;
using ShopFlower.IService.ServiceProduct;
using ShopFlower.IService.ServiceUser;

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

        public IActionResult Index()
        {
            var product = _productService.GetProductShort(1,0);
            return View();
        }

        public IActionResult ListProduct()
        {

            return View();
        }
    }
}

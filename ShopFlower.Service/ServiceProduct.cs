using Microsoft.EntityFrameworkCore;
using ShopFlower.Data;
using ShopFlower.Data.Models;
using ShopFlower.IService.ServiceProduct;

namespace ShopFlower.Service
{
    public class ServiceProduct : IProductService
    {
        private DbContextOptions<ApplicationContext> _DBOptions;

        public ServiceProduct(DbContextOptions<ApplicationContext> dbOptions)
        {
            this._DBOptions = dbOptions;
        }

        public async Task<Product> GetProduct(int productId)
        {
            await using var db = new ApplicationContext(_DBOptions);

            var product = db.Products.FirstOrDefault(s => s.Id == productId);

            if(product == null)
                throw new Exception("Нет такого товара");

            return product;
        }

        public async Task<List<ShortProduct>> GetProductByFilter(ProductFilter filter)
        {
            await using var db = new ApplicationContext(_DBOptions);

            var productFilter = await db.Products.Select(c => ShortProduct.ConvertToShortProduct(c))
              .ToListAsync();

            if (filter == null || productFilter == null) throw new Exception("Пусто");

            if (filter.PriceMax != 2000 || filter.PriceMin != 0)
                productFilter = productFilter.Where(f => f.Price < filter.PriceMax && f.Price > filter.PriceMin).ToList();

            return productFilter;
        }

        public async Task<List<ShortProduct>> GetProductShort(int take, int skip)
        {
            await using var db = new ApplicationContext(_DBOptions);

            var products = db.Products.Skip(skip).Take(take).Select(x => ShortProduct.ConvertToShortProduct(x)).ToList();

            if (products.Count == 0 || products == null)
                throw new Exception("Список товара пуст");

            return products;
        }
    }
}

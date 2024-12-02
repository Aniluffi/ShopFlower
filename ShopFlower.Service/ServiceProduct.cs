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

        public Task<Product> GetProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ShortProduct>> GetProductShort(int take, int skip)
        {
            await using var db = new ApplicationContext(_DBOptions);

            return db.Products.Skip(skip).Take(take).Select(x => ShortProduct.ConvertToShortProduct(x));
        }
    }
}

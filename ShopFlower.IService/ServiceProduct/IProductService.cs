using ShopFlower.Data.Models;

namespace ShopFlower.IService.ServiceProduct
{
    public interface IProductService
    {
        Task<Product> GetProduct(int productId);
        Task<List<ShortProduct>> GetProductShort(int take,int skip);
    }
}

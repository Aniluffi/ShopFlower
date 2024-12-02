using ShopFlower.IService.ServiceCart;

namespace ShopFlower.Models
{
    public class ListProduct
    {
        public List<ShortProduct>? Products { get; set; } = new List<ShortProduct>();
    }
}

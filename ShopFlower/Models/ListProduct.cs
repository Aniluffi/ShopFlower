using ShopFlower.IService.ServiceProduct;

namespace ShopFlower.Models
{
    public class ListProduct
    {
        public List<ShortProduct>? Products { get; set; }
        public ProductFilter? ProductFilter { get; set; }
    }
}

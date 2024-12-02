using ShopFlower.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFlower.IService.ServiceProduct
{
    public class ShortProduct
    {
        public string? Name { get; set; }
        public int Price {  get; set; }

        public static ShortProduct ConvertToShortProduct(Product product)
        {
            return new ShortProduct
            {
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}

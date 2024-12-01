using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFlower.Data.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int TotalSum { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public int userId { get; set; }
        public User? User { get; set; }
    }
}

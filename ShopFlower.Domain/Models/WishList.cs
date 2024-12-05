using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFlower.Data.Models
{
    public class WishList
    {
        public int Id { get; set; }
        public int userId {  get; set; }
        public User? User { get; set; }
        public int ProductId { get; set; }
        public Product? Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFlower.Data.Models
{
    public class WishList
    {
        [Key]
        public int Id { get; set; }
        public User? User { get; set; } 
        public List<Product>? Products { get; set; } 
    }
}

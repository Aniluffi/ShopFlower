using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFlower.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DescriptionShort { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public string? Img { get; set; }
        public List<Cart>? Carts { get; set; } = new List<Cart>();
        public List<WishList>? WishList { get; set; } = new List<WishList>();
        public List<Order>? Orders { get; set; } = new List<Order>();
    }
}

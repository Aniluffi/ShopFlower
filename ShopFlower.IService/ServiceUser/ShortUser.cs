using ShopFlower.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFlower.IService.ServiceUser
{
    public class ShortUser
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthOfDay { get; set; }
        public List<WishList>? WishList { get; set; }
    }
}

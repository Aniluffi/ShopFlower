using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFlower.Data.Models
{
    public class Addresses
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public string SecondName {  get; set; } = string.Empty;
        public string City {  get; set; } = string.Empty;
        public string State {  get; set; } = string.Empty;
        public int ZIP { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}

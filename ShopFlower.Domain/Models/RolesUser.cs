using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ShopFlower.Data.Models
{
    public class RolesUser
    {
        public EnumTypeRole RoleId { get; set; }
        public Roles? Role {  get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}

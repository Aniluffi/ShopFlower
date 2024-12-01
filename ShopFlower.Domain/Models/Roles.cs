using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum EnumTypeRole
{
    Guest,
    User,
    Admin
}
namespace ShopFlower.Data.Models
{


    public class Roles
    {
        [Key]
        public EnumTypeRole Role {  get; set; }
        public string Name { get; set; } = string.Empty;
        public List<RolesUser>? RolseUsers { get; set; }
    }
}

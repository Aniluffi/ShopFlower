using ShopFlower.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFlower.IService.ServiceUser
{
    public interface IServiceUser
    {
        Task<User> SearchUserEmail(string email);
        Task<IEnumerable<Exception>> AddUser(User user);
        Task<IEnumerable<Exception>> DeleteUser(int userId);
        Task<IEnumerable<Exception>> UpdateUser(int userId,string name,string secondName ,string login, DateTime bDay);
        Task<IEnumerable<Exception>> UpdatePassword(int userId, string password);
        Task<IEnumerable<Exception>> AddRole(EnumTypeRole roleId, int userId);
        Task<IEnumerable<Exception>> DeleteRole(int userId, EnumTypeRole roleId);
        Task<IEnumerable<Exception>> UpdateAddresses(int userId, string name, string secondName, string city, string state, int ZIP);
        Task<IEnumerable<Exception>> DeleteAddresses(int userId);
        Task<IEnumerable<Exception>> AddProductInWishList(int userId, int productId);
        Task<IEnumerable<Exception>> AddProductInCart(int userId, int productId);
        Task<Cart> GetProductInCart(int userId);
        Task<IEnumerable<Exception>> AddProductInOrders(int userId, int productId);
        Task<IEnumerable<Exception>> RemoveProductInWishList(int userId, int productId);
        Task<IEnumerable<Exception>> RemoveProductInCart(int userId, int productId);
        Task<IEnumerable<Exception>> RemoveProductInOrders(int userId, int productId);
        Task<List<ShortUser>> GetShortUser(int userId);
        Task<User> GetUser(string email,string password);
    }
}

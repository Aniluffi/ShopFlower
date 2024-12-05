using Microsoft.EntityFrameworkCore;
using Npgsql;
using ShopFlower.Data;
using ShopFlower.Data.Models;
using ShopFlower.IService.ServiceUser;
using System.Data;

namespace ShopFlower.Service
{
    public class ServiceUser : IServiceUser
    {
        private DbContextOptions<ApplicationContext> _DBOptions;

        public ServiceUser(DbContextOptions<ApplicationContext> dbOptions)
        {
            this._DBOptions = dbOptions;
        }

        public async Task<IEnumerable<Exception>> AddProductInCart(int userId, int productId)
        {

            var list = new List<Exception>();
            try
            {

                await using var dB = new ApplicationContext(this._DBOptions);

                var user = await dB.Users.FirstOrDefaultAsync(c => c.Id == userId);
                var product = await dB.Products.FirstOrDefaultAsync(c => c.Id == productId);

                if (user == null) list.Add(new Exception($"Нет такого пользователя"));
                if (product == null) list.Add(new Exception($"Нет такого товара"));

                if (list.Count != 0) return list;

                user.Carts.Add(new Cart
                {
                    TotalSum = product.Price * product.Quantity,
                    Products = product
                });
                await dB.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Ошибка при обновлении базы данных" + ex.Message);
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Ошибка SQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return list;
        }

        public Task<IEnumerable<Exception>> AddProductInOrders(int userId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Exception>> AddProductInWishList(int userId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Exception>> AddRole(EnumTypeRole roleId, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Exception>> AddUser(User user)
        {
            var list = new List<Exception>();
            try
            {
                list.AddRange(User.IsValidLogin(user.Login));
                list.AddRange(User.IsValidPassword(user.Password));
                list.AddRange(User.IsValidEmail(user.Email));

                await using var dB = new ApplicationContext(this._DBOptions);

                if (dB.Users.Any(c => c.Email == user.Email)) list.Add(new Exception($"Почта {user.Email} зарегистрирована"));

                if (list.Count != 0) return list;

                dB.Users.Add(user);
                await dB.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Ошибка при обновлении базы данных" + ex.Message);
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Ошибка SQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return list;
        }

        public Task<IEnumerable<Exception>> DeleteAddresses(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Exception>> DeleteRole(int userId, EnumTypeRole roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Exception>> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cart>> GetProductInCart(int userId)
        {
            try
            {
               await using var db = new ApplicationContext(_DBOptions);

                var user = await db.Users.FirstAsync(c => c.Id == userId);


                if (user != null)
                {
                    return user.Carts;
                }
                else throw new Exception("Пользователя нет");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Ошибка при обновлении базы данных" + ex.Message);
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Ошибка SQL: " + ex.Message);
            }
            
            return new List<Cart>();
        }

        public Task<List<ShortUser>> GetShortUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(string email, string password)
        {
            try
            {
                var db = new ApplicationContext(_DBOptions);

                if (db.Users.Any(c => c.Email == email && c.Password == password))
                {
                    return await db.Users.FirstAsync(c => c.Email == email && c.Password == password);
                }
                else throw new Exception("Неправильный логин или пароль");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Ошибка при обновлении базы данных" + ex.Message);
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Ошибка SQL: " + ex.Message);
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
            return new User();
        }

        public Task<IEnumerable<Exception>> RemoveProductInCart(int userId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Exception>> RemoveProductInOrders(int userId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Exception>> RemoveProductInWishList(int userId, int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> SearchUserEmail(string email)
        {
            try
            {
                var db = new ApplicationContext(_DBOptions);

                if (db.Users.Any(c => c.Email == email))
                {
                    return await db.Users.FirstAsync(c => c.Email == email);
                }
                else throw new Exception("Нет такого пользователя");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Ошибка при обновлении базы данных" + ex.Message);
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Ошибка SQL: " + ex.Message);
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
            return new User();
        }

        public Task<IEnumerable<Exception>> UpdateAddresses(int userId, string name, string secondName, string city, string state, int ZIP)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Exception>> UpdatePassword(int userId, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Exception>> UpdateUser(int userId, string name, string secondName, string login, DateTime bDay)
        {
            throw new NotImplementedException();
        }
    }
}

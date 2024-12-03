﻿using Microsoft.EntityFrameworkCore;
using ShopFlower.Data;
using ShopFlower.Data.Models;
using ShopFlower.IService.ServiceProduct;

namespace ShopFlower.Service
{
    public class ServiceProduct : IProductService
    {
        private DbContextOptions<ApplicationContext> _DBOptions;

        public ServiceProduct(DbContextOptions<ApplicationContext> dbOptions)
        {
            this._DBOptions = dbOptions;
        }

        public async Task<Product> GetProduct(int productId)
        {
            await using var db = new ApplicationContext(_DBOptions);
            return db.Products.FirstOrDefault(s => s.Id == productId);
        }

        public async Task<List<ShortProduct>> GetProductShort(int take, int skip)
        {
            await using var db = new ApplicationContext(_DBOptions);

           return db.Products.Skip(skip).Take(take).Select(x => ShortProduct.ConvertToShortProduct(x)).ToList();
        }
    }
}

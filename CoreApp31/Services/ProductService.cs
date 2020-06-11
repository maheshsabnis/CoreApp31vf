using CoreApp31.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreApp31.Services
{
    public class ProductService : IService<Product, int>
    {
        private readonly VodafoneDbContext ctx;
        /// <summary>
        /// Inject the VodafoneDbContext in the Service class using ctor injection 
        /// </summary>
        public ProductService(VodafoneDbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Product> CreateAsync(Product entity)
        {
            var res = await ctx.Products.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var c = await ctx.Products.FindAsync(id);
            if (c != null)
            {
                ctx.Products.Remove(c);
                await ctx.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await ctx.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            var cat = await ctx.Products.FindAsync(id);
            return cat;
        }

        public async Task<Product> UpdateAsync(int id, Product entity)
        {
            var c = await ctx.Products.FindAsync(id);
            if (c != null)
            {
                c.ProductId = entity.ProductId;
                c.ProductName = entity.ProductName;
                c.Manufacturer = entity.Manufacturer;
                c.Description = entity.Description;
                c.Price = entity.Price;
                c.CategoryRowId = entity.CategoryRowId;

                // 2. Update using Cursor State
              //  ctx.Entry<Product>(entity).State = EntityState.Modified;
                await ctx.SaveChangesAsync();

            }
            return entity;
        }
    }
}

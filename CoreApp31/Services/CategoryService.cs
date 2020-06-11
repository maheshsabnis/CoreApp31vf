using CoreApp31.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreApp31.Services
{
    public class CategoryService : IService<Category, int>
    {
        private readonly VodafoneDbContext ctx;
        /// <summary>
        /// Inject the VodafoneDbContext in the Service class using ctor injection 
        /// </summary>
        public CategoryService(VodafoneDbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Category> CreateAsync(Category entity)
        {
            var res = await ctx.Categories.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var c = await ctx.Categories.FindAsync(id);
            if (c != null)
            {
                ctx.Categories.Remove(c);
                await ctx.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await ctx.Categories.ToListAsync();
        }

        public async Task<Category> GetAsync(int id)
        {
            var cat = await ctx.Categories.FindAsync(id);
            return cat;
        }

        public async Task<Category> UpdateAsync(int id, Category entity)
        {
            var c = await ctx.Categories.FindAsync(id);
            if (c != null)
            {
                c.CategoryId = entity.CategoryId;
                c.CategoryName = entity.CategoryName;
                c.BasePrice = entity.BasePrice;

                // 2. Update using Cursor State
              //  ctx.Entry<Category>(entity).State = EntityState.Modified;
                await ctx.SaveChangesAsync();

            }
            return entity;
        }
    }
}

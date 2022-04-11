using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Models;

namespace WebApplication.Repository
{
    public class BaseRepository<T>: IBaseRepository<T> where T:Base
    {
        protected ShopContext Context;

        public virtual async Task<List<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(long id)
        {
            var res = await Context.Set<T>().FirstOrDefaultAsync(t => t.Id == id); 
            if (res == null)
                throw new Exception($"Cannot find {typeof(T).Name} with id: {id}");
            return res;
        }

        public virtual async Task<T> Add(T entity)
        {
            if (!await IsExists(entity))
            {
                var res = await Context.AddAsync(entity);
                await Context.SaveChangesAsync();
                return res.Entity;   
            }

            throw new Exception($"Same {typeof(T).Name} already exists");
        }

        public virtual async Task<T> Update(T entity)
        {
            if (await IsExists(entity))
            {
                await GetById(entity.Id);
                var res = Context.Update(entity);
                await Context.SaveChangesAsync();
                return res.Entity;   
            }
            throw new Exception($"Cannot find this {typeof(T).Name}");
        }

        public async Task Delete(T entity)
        {
            if (await IsExists(entity))
            {
                Context.Set<T>().Remove(entity);
                await Context.SaveChangesAsync();   
            }
            else
            {
                throw new Exception($"Cannot find this {typeof(T).Name}");
            }
        }

        public virtual async Task<bool> IsExists(T entity)
        {
            return false;
        }
    }
}
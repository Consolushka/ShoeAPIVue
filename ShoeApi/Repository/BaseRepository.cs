using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Models;
using Type = WebApplication.Data.Models.Type;

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

        public async Task<T> Add(T entity)
        {
            var res = await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return res.Entity;
        }

        public virtual async Task<T> Update(T entity)
        {
            await GetById(entity.Id);
            var res = Context.Update(entity);
            await Context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public virtual Task<bool> IsExists(string name)
        {
            return null;
        }
    }
}
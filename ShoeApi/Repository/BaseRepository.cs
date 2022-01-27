using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
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
            CheckForId(id);
            return await Context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<T> Add(T entity)
        {
            var res = await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<T> Update(T entity)
        {
            CheckForId(entity.Id);
            var res = Context.Update(entity);
            await Context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task Delete(long id)
        {
            CheckForId(id);
            Context.Set<T>().Remove(await GetById(id));
            await Context.SaveChangesAsync();
        }

        protected void CheckForId(long id)
        {
            if (Context.Set<T>().FirstOrDefault(t => t.Id == id) == null)
                throw new Exception($"Cannot find {typeof(T).Name} with id: {id}");
        }
    }
}
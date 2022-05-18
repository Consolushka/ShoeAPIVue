using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Shop.Repositories
{
    internal class BaseRepository<T>: IBaseRepository<T> where T:Base
    {
        protected ShopContext Context;

        public virtual async Task<List<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(long id)
        {
            await CheckForExistingId(id);
            var res = await Context.Set<T>().FirstOrDefaultAsync(t => t.Id == id); 
            return res;
        }

        public virtual async Task<T> Add(T entity)
        {
            if (!await IsAlreadyExists(entity))
            {
                var res = await Context.AddAsync(entity);
                await Context.SaveChangesAsync();
                return res.Entity;   
            }

            throw new Exception($"Same {typeof(T).Name} already exists");
        }

        public virtual async Task<T> Update(T entity)
        {
            await CheckForExistingId(entity.Id);
            if (!await IsAlreadyExists(entity))
            {
                var res = Context.Update(entity);
                await Context.SaveChangesAsync();
                return res.Entity;   
            }
            throw new Exception($"Same {typeof(T).Name} already exists");
        }

        public async Task Delete(long id)
        {
            await CheckForExistingId(id);
            Context.Set<T>().Remove(await Context.Set<T>().FindAsync(id));
            await Context.SaveChangesAsync();
        }

        public virtual async Task<bool> IsAlreadyExists(T entity)
        {
            return false;
        }

        public async Task CheckForExistingId(long id)
        {
            if (await Context.Set<T>().FirstOrDefaultAsync(t => t.Id == id) == null)
            {
                throw new Exception($"Cannot find {typeof(T).Name} with id: {id}");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data;
using Shop.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Shop.Repositories.Basis
{
    internal class BaseRepository<T>: IBaseRepository<T> where T:Base
    {
        protected ShopContext Context;

        public BaseRepository(ShopContext _context)
        {
            Context = _context;
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(long id)
        {
            var res = await Context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
            return res;
        }

        public virtual async Task<T> Add(T entity)
        {
            var res = await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return res.Entity;
        }

        public virtual async Task<T> Update(T entity)
        {
            var res = Context.Update(entity);
            await Context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task Delete(long id)
        {
            var current = await Context.Set<T>().FindAsync(id);
            if (current == null)
            {
                throw new System.NullReferenceException();
            }
            Context.Set<T>().Remove(current);
            await Context.SaveChangesAsync();
        }
    }
}
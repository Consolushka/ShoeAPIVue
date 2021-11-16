using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Exceptions;

namespace WebApplication.Repository
{
    public class BaseRepository<T>: IBaseRepository<T> where T:Base
    {
        protected ShoeContext Context;

        public List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public T GetById(long id)
        {
            CheckForId(id);
            return Context.Set<T>().FirstOrDefault(t => t.Id == id);
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

        public void Delete(long id)
        {
            CheckForId(id);
            Context.Set<T>().Remove(GetById(id));
            Context.SaveChanges();
        }

        private void CheckForId(long id)
        {
            if (Context.Set<T>().FirstOrDefault(t => t.Id == id) == null)
                throw new NullEntityException($"Cannot find {typeof(T).Name} with id:", id);
        }
    }
}
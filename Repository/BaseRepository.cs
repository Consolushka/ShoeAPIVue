using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Repository
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
            return Context.Set<T>().FirstOrDefault(t => t.Id == id);
        }

        public async Task<T> Add(T entity)
        {
            var res = await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return res.Entity;
        }
    }
}
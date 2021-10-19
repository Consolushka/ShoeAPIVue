using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Repository
{
    public class BaseRepository<T>: IBaseRepository<T> where T:Base
    {
        private readonly ShoeContext _context;

        public BaseRepository(ShoeContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(long Id)
        {
            return _context.Set<T>().FirstOrDefault(t => t.Id == Id);
        }

        public long Add(T entity)
        {
            var res = _context.Add(entity);
            _context.SaveChanges();
            return res.Entity.Id;
        }
    }
}
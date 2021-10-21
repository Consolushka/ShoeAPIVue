using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public T GetById(long id)
        {
            return _context.Set<T>().FirstOrDefault(t => t.Id == id);
        }

        public async Task<long> Add(T entity)
        {
            var res = await _context.AddAsync(entity);
            _context.SaveChanges();
            return res.Entity.Id;
        }
    }
}
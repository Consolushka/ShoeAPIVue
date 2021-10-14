using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoeAPIVue.Data;
using ShoeAPIVue.Entities;
using ShoeAPIVue.Models;

namespace ShoeAPIVue.Services
{
    public class UserRepository<T> : IEfRepository<T> where T : Base
    {
        private readonly ShoeContext _context;

        public UserRepository(ShoeContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(long id)
        {
            return _context.Set<T>().FirstOrDefault(f => f.Id == id);
        }
        
        public async Task<long> Add(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }
    }
} 
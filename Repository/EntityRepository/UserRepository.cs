using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Repository.EntityRepository
{
    public class UserRepository: IUserRepository
    {
        
        private readonly ShoeContext _context;

        public UserRepository(ShoeContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.User.ToList();
        }

        public User GetById(long id)
        {
            return _context.User.FirstOrDefault(t => t.Id == id);
        }

        public async Task<long> Add(User entity)
        {
            var res = await _context.AddAsync(entity);
            _context.SaveChanges();
            return res.Entity.Id;
        }
    }
}
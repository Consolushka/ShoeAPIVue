using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

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
            foreach (var user in await _context.User.ToListAsync())
            {
                if (entity.Email == user.Email)
                {
                    return 0;
                }
            }
            var res = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return res.Entity.Id;
        }
    }
}
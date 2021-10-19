using System.Collections.Generic;
using System.Linq;
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

        public User GetById(long Id)
        {
            return _context.User.FirstOrDefault(t => t.Id == Id);
        }

        public long Add(User entity)
        {
            var res = _context.Add(entity);
            _context.SaveChanges();
            return res.Entity.Id;
        }
    }
}
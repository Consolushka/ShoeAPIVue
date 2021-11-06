using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

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

        public async Task<User> Add(User entity)
        {
            foreach (var user in await _context.User.ToListAsync())
            {
                if (entity.Email == user.Email)
                {
                    return null;
                }
            }
            var res = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<User> GetByKey(Guid key)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.ConfirmString == key && u.IsConfirmed == false);
        }

        public void ConfirmUser(User user)
        {
            user.IsConfirmed = true;
            _context.User.Update(user);
            _context.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository.EntityRepository
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {

        public UserRepository(ShoeContext context)
        {
            Context = context;
        }

        public new async Task<User> Add(User entity)
        {
            foreach (var user in await Context.User.ToListAsync())
            {
                if (entity.Email == user.Email)
                {
                    return null;
                }
            }
            var res = await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<User> GetByKey(Guid key)
        {
            return await Context.User.FirstOrDefaultAsync(u => u.ConfirmString == key && u.IsConfirmed == false);
        }

        public void ConfirmUser(User user)
        {
            user.IsConfirmed = true;
            Context.User.Update(user);
            Context.SaveChanges();
        }
    }
}
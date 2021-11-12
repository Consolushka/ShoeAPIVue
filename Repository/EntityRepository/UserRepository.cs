using System;
using System.Threading.Tasks;
using Entities.Models;
using Entities.Support;
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
            var res = await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<User> GetByKey(Guid key)
        {
            return await Context.User.FirstOrDefaultAsync(u => u.ConfirmString == key && u.IsActive == false);
        }

        public void ConfirmUser(User user)
        {
            user.IsActive = true;
            Context.User.Update(user);
            Context.SaveChanges();
        }
    }
}
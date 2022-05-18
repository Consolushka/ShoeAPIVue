using System;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.DataBase;
using Microsoft.EntityFrameworkCore;
using Shop.Repositories.Contracts;

namespace Shop.Repositories.ModelRepositories
{
    internal class UserRepository: BaseRepository<User>, IUserRepository
    {

        public UserRepository(ShopContext context)
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
            return await Context.Users.FirstOrDefaultAsync(u => u.ConfirmString == key && u.IsActive == false);
        }

        public void ConfirmUser(User user)
        {
            user.IsActive = true;
            Context.Users.Update(user);
            Context.SaveChanges();
        }
    }
}
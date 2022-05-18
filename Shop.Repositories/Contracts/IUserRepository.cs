using System;
using System.Threading.Tasks;
using Shop.Data.Models;

namespace Shop.Repositories.Contracts
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User> GetByKey(Guid key);
        void ConfirmUser(User user);
    }
}
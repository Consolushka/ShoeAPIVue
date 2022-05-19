using System;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Basis;

namespace Shop.Repositories.Contracts
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User> GetByKey(Guid key);
        void ConfirmUser(User user);
        Task<User> GetByUserNameOrEmailAndPassword(User user);
        Task<User> GetByUsernameOrEmail(User user);
    }
}
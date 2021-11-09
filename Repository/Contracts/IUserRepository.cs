using System;
using System.Threading.Tasks;
using Entities.Models;
using Entities.Support;

namespace Repository.Contracts
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User> GetByKey(Guid key);
        void ConfirmUser(User user);
    }
}
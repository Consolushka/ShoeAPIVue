using System;
using System.Threading.Tasks;
using WebApplication.Data.Models;

namespace WebApplication.Repository.Contracts
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User> GetByKey(Guid key);
        void ConfirmUser(User user);
    }
}
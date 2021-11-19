using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;

namespace WebApplication.Services.Contracts
{
    public interface IUserService
    {
        UserResponse Authenticate(UserVM vm);
        Task<User> Register(UserVM userVm);
        Task<bool> ConfirmUser(Guid key);
        List<User> GetAll();
        User GetById(long Id);
        Task<User> Update(UserVM user, long id);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;

namespace WebApplication.Services.Contracts
{
    public interface IUserService
    {
        Task<UserResponse> Authenticate(UserVM vm);
        Task<User> Register(UserVM userVm);
        Task<bool> ConfirmUser(Guid key);
        Task<List<User>> GetAll();
        Task<User> GetById(long Id);
        Task<User> Update(UserVM user, long id);
    }
}
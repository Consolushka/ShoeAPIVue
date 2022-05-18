using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Data.ViewModels;

namespace Shop.Services.Contracts
{
    public interface IUserService
    {
        Task<UserResponse> Authenticate(UserVM vm);
        Task<User> Register(UserVM userVm);
        Task<bool> ConfirmUser(Guid key);
        Task<List<User>> GetAll();
        User GetById(long Id);
        Task<User> Update(UserVM user, long id);
    }
}
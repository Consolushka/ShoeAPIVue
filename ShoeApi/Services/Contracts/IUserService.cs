using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;

namespace WebApplication.Services.Contracts
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<User> Register(UserModel userModel);
        Task<bool> ConfirmUser(Guid key);
        List<User> GetAll();
        User GetById(long Id);
        Task<User> Update(User user);
    }
}
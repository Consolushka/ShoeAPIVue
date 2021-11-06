using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.Support;

namespace Core.Contracts
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<User> Register(UserModel userModel);
        Task<bool> ConfirmRegistration(Guid key);
        List<User> GetAll();
        User GetById(long Id);
    }
}
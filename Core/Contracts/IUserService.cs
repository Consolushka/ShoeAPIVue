using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.Support;

namespace Core
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<AuthenticateResponse> Register(UserModel userModel);
        List<User> GetAll();
        User GetById(long Id);
    }
}
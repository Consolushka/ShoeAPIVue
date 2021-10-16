using System.Threading.Tasks;
using ShoeAPIVue.Entities;
using ShoeAPIVue.Models;

namespace ShoeAPIVue.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<AuthenticateResponse> Register(UserModel user);
        User GetById(long id);
    }
}
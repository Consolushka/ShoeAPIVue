using WebApplication.Data.Models;

namespace WebApplication.Data.ViewModels
{
    public class AuthenticateResponse
    {
        
        public long Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            UserName = user.UserName;
            Token = token;
        }
    }
}
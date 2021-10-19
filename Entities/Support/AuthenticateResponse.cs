using Entities.Models;

namespace Entities.Support
{
    public class AuthenticateResponse
    {
        
        public long Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            Token = token;
        }
    }
}
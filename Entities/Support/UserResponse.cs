using Entities.Models;

namespace Entities.Support
{
    public class UserResponse
    {
        public long Id { get; set;}
        public string Email { get; set;}
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }

        public UserResponse(User u)
        {
            Id = u.Id;
            Email = u.Email;
            UserName = u.UserName;
            IsAdmin = u.IsAdmin;
        }
    }
}
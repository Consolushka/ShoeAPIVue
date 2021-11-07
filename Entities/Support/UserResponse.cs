using Entities.Models;

namespace Entities.Support
{
    public class UserResponse
    {
        public long Id { get; set;}
        public string Email { get; set;}
        public string UserName { get; set; }
        public long RoleId { get; set; }

        public UserResponse(User u)
        {
            Id = u.Id;
            Email = u.Email;
            UserName = u.UserName;
            RoleId = u.RoleId;
        }
    }
}
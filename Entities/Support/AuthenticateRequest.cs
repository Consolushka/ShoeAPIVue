using System.ComponentModel.DataAnnotations;

namespace Entities.Support
{
    public class AuthenticateRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
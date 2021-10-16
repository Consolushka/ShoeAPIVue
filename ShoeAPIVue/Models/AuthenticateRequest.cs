using System.ComponentModel.DataAnnotations;

namespace ShoeAPIVue.Models
{
    public class AuthenticateRequest
    {
        [Required] 
        public string Email { get; set; }

        [Required] 
        public string Password { get; set; }
    }
}
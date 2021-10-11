using System.ComponentModel.DataAnnotations;

namespace ShoeAPIVue.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Mail not specified")]
        public string Email { get; set;}
        
        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set;}
        
        [Required(ErrorMessage = "Not correct Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set;}
    }
}
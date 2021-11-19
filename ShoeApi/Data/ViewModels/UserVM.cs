namespace WebApplication.Data.ViewModels
{
    public class UserVM
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserVM(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
namespace WebApplication.Data.ViewModels
{
    public class UserVM
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserVM() {}

        public UserVM(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
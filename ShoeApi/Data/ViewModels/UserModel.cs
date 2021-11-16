namespace WebApplication.Data.ViewModels
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
        public bool IsActive { get; set; }

        public UserModel(string email, string password)
        {
            Id = 0;
            Email = email;
            Password = password;
            RoleId = 1;
            IsActive = false;
        }
    }
}
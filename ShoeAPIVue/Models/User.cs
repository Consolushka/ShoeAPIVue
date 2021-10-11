namespace ShoeAPIVue.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(RegisterModel model)
        {
            Email = model.Email;
            Password = model.Password;
        }
    }
}
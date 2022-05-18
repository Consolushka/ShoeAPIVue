using System;
using System.Text.Json.Serialization;
using Shop.Data.ViewModels;

namespace Shop.Data.Models
{
    public class User: Base
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public Guid ConfirmString { get; set; }

        public void FillFromVM(UserVM vm)
        {
            Email = vm.Email;
            Address = vm.Address;
            UserName = vm.UserName;
            Password = vm.Password;
            IsActive = false;
        }
    }
}
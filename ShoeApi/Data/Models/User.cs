using System;
using System.Text.Json.Serialization;

namespace WebApplication.Data.Models
{
    public class User: Base
    {
        public string Email { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public Guid ConfirmString { get; set; }
    }
}
using System;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class User: Base
    {
        public string Email { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public long RoleId { get; set; }
        public Role Role { get; set;}
        public bool IsConfirmed { get; set; }
        public Guid ConfirmString { get; set; }
    }
}
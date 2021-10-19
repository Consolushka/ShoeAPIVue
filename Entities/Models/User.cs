﻿using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class User: Base
    {
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
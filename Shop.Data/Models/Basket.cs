using System.Text.Json.Serialization;

namespace Shop.Data.Models
{
    public class Basket: Base
    {
        public long UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set;}
    }
}
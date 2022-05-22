using System;
using System.Text.Json.Serialization;

namespace Shop.Data.Models
{
    public class Order: Base
    {
        public long UserId { get; set;}
        [JsonIgnore]
        public User? User { get; set; }
        public DateTime OrderTime { get; set; }
        public short Status { get; set;}
        public bool IsPaid { get; set;}

        public bool IsNull()
        {
            if (Id == 0 && UserId==0 && OrderTime==DateTime.MinValue && Status==0)
            {
                return true;
            }

            return false;
        }
    }
}
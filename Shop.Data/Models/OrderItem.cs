using System.Text.Json.Serialization;

namespace Shop.Data.Models
{
    public class OrderItem: Base
    {
        public long OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
        public double Cost { get; set; }
        public int Count { get; set; }
        public long StockItemId { get; set; }
        [JsonIgnore]
        public StockItem? StockItem { get; set; }
    }
}
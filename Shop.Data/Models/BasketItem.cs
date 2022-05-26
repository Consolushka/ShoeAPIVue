using System.Text.Json.Serialization;

namespace Shop.Data.Models
{
    public class BasketItem: Base
    {
        public long BasketId { get; set; }
        [JsonIgnore]
        public Basket? Basket { get; set; }
        public double Cost { get; set; }
        public int Count { get; set; }
        public long StockItemId { get; set; }
        [JsonIgnore]
        public StockItem? StockItem { get; set; }
    }
}
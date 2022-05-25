using System.Text.Json.Serialization;

namespace Shop.Data.Models
{
    public class StockItem: Base
    {
        public long GoodId { get; set; }
        [JsonIgnore]
        public Good? Good { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public long StoreId { get; set; }
        [JsonIgnore]
        public Store? Store { get; set; }
    }
}
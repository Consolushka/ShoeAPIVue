using System.Collections.Generic;

namespace Shop.Data.Models
{
    public class Store: Base
    {
        public string Name { get; set; }
        public string Address { get; set; }
        
        public List<StockItem> StockItems { get; set; }
    }
}
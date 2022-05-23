using System.Collections.Generic;
using System.Text.Json.Serialization;
using Shop.Data.ViewModels;

namespace Shop.Data.Models
{
    public class Good: BaseProductModel
    {
        [JsonIgnore]
        public long TypeId { get; set; }
        public Type Type { get; set; }
        [JsonIgnore]
        public long BrandId { get; set;}
        public Brand Brand { get; set; }
        public string PhotoFileName { get; set; }
        
        public List<StockItem> StockItems { get; set; }

        public void FillFromVm(GoodVm goodVm)
        {
            Name = goodVm.Name;
            BrandId = goodVm.BrandId;
            TypeId = goodVm.TypeId;
            PhotoFileName = goodVm.PhotoFileName;
        }
    }
}
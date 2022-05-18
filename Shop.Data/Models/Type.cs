using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shop.Data.Models
{
    public class Type: BaseProductModel
    {
        //Nav props
        public List<Good> Goods { get; set; }
        //Nav props
        [JsonIgnore]
        public List<BrandType> BrandTypes { get; set; }
    }
}
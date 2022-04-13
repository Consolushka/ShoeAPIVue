using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApplication.Data.Models
{
    //Describes types of goods 
    public class Type: BaseProductModel
    {
        
        //Nav props
        public List<Good> Goods { get; set; }
        //Nav props
        [JsonIgnore]
        public List<BrandType> BrandTypes { get; set; }
    }
}
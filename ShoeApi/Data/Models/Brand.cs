using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication.Data.Models
{
    public class Brand: BaseProductModel
    {
        //Nav Prop
        public List<Good> Goods { get; set; }
        [JsonIgnore]
        public List<BrandType> BrandTypes { get; set; }
        //Nav Prop
        [NotMapped]
        public List<Type> Types { get; set; }
    }
}
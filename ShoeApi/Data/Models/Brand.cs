using System.Collections.Generic;

namespace WebApplication.Data.Models
{
    public class Brand: BaseProductModel
    {
        //Nav Prop
        public List<Good> Goods { get; set; }
    }
}
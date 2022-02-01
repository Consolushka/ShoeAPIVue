using System.Collections.Generic;

namespace WebApplication.Data.Models
{
    //Describes types of goods 
    public class Type: BaseProductModel
    {
        
        //Nav props
        public List<Good> Goods { get; set; }
        public List<BrandType> BrandTypes { get; set; }
    }
}
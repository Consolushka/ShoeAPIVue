using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Data.Models
{
    //Describes types of goods 
    public class Type: BaseProductModel
    {
        
        //Nav props
        public List<Good> Goods { get; set; }
        //Nav props
        public List<BrandType> BrandTypes { get; set; }
    }
}
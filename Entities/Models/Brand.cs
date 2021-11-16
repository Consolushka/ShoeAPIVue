using System.Collections.Generic;

namespace Entities.Models
{
    public class Brand: Base
    {
        public string Name { get; set; }
        
        //Nav Prop
        public List<Shoe> Shoes { get; set; }
    }
}
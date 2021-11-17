﻿using System.Collections.Generic;

namespace WebApplication.Data.Models
{
    public class Brand: Base
    {
        public string Name { get; set; }
        
        //Nav Prop
        public List<Shoe> Shoes { get; set; }
    }
}
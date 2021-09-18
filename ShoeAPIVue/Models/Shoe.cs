﻿using System;
using ShoeAPIVue.Data;

namespace ShoeAPIVue.Models
{
    public class Shoe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public DateTime CreationTime { get; set; }
        public string PhotoFileName { get; set; }
        
        
        public Shoe FillBrand(ShoeContext context)
        {
            Brand = context.Brand.Find(BrandId);
            return this;
        }
    }
}

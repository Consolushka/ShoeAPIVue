using System;

namespace WebApplication.Data.Models
{
    public class Shoe: Base
    {
        public string Name { get; set; }
        public long BrandId { get; set;}
        public Brand Brand { get; set; }
        public DateTime CreationTime { get; set; }
        public string PhotoFileName { get; set; }
    }
}
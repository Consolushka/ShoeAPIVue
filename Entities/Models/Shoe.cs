using System;

namespace Entities.Models
{
    public class Shoe: Base
    {
        public string Name { get; set; }
        public Brand Brand { get; set; }
        public DateTime CreationTime { get; set; }
        public string PhotoFileName { get; set; }
    }
}
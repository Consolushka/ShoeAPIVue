using System;

namespace WebApplication.Data.ViewModels
{
    public class ShoeVM
    {
        public string Name { get; set; }
        public long BrandId { get; set; }
        public DateTime CreationTime { get; set; }
        public string PhotoFileName { get; set; }
    }
}
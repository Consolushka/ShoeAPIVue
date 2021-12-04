using System;
using WebApplication.Data.ViewModels;

namespace WebApplication.Data.Models
{
    public class Shoe: Base
    {
        public string Name { get; set; }
        public long BrandId { get; set;}
        public Brand Brand { get; set; }
        public DateTime CreationTime { get; set; }
        public string PhotoFileName { get; set; }

        public void Update(ShoeVM shoeVm)
        {
            Name = shoeVm.Name;
            BrandId = shoeVm.BrandId;
            CreationTime = shoeVm.CreationTime;
            PhotoFileName = shoeVm.PhotoFileName;
        }
    }
}
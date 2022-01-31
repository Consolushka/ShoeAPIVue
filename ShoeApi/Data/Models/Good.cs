using System;
using WebApplication.Data.ViewModels;

namespace WebApplication.Data.Models
{
    public class Good: Base
    {
        public string Name { get; set; }
        public long BrandId { get; set;}
        public Brand Brand { get; set; }
        public string PhotoFileName { get; set; }

        public void FillFromVm(GoodVm goodVm)
        {
            Name = goodVm.Name;
            BrandId = goodVm.BrandId;
            PhotoFileName = goodVm.PhotoFileName;
        }
    }
}
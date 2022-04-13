using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication.Data.ViewModels;

namespace WebApplication.Data.Models
{
    public class Good: BaseProductModel
    {
        public long TypeId { get; set; }
        public Type Type { get; set; }
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
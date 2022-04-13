namespace WebApplication.Data.Models
{
    public class BrandType: Base
    {
        public long BrandId { get; set; }
        public Brand Brand { get; set; }
        public long TypeId { get; set; }
        public Type Type { get; set; }
    }
}
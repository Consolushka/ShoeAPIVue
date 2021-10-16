using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoeAPIVue.Data;

namespace ShoeAPIVue.Entities
{
    public class Shoe: Base
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public DateTime CreationTime { get; set; }
        public string PhotoFileName { get; set; }
        
        
        public async Task FillBrand(ShoeContext context)
        {
            Brand = await context.Brand.FirstOrDefaultAsync(b=>b.Id == BrandId);
        }
    }
}

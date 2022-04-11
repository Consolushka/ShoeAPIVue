using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;

namespace WebApplication.Repository.EntityRepository
{
    public class BrandRepository: BaseRepository<Brand>, IBrandRepository
    {   
        public BrandRepository(ShopContext context)
        {
            Context = context;
        }

        public override async Task<bool> IsExists(Brand brand)
        {
            if (await Context.Brands.FirstOrDefaultAsync(b => b.Name == brand.Name) == null)
            {
                return false;
            }

            return true;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
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

        public override async Task<List<Brand>> GetAll()
        {
            var res = await Context.Brands.ToListAsync();
            List<BrandType> brandTypes = await Context.BrandTypes.Include(e => e.Brand).ToListAsync();
            foreach (var brand in res)
            {
                List<Type> types = new List<Type>();
                foreach (var bt in brandTypes)
                {
                    if (bt.Brand.Id == brand.Id)
                    {
                        types.Add(bt.Type);
                    }
                }
                foreach (var brandType in brandTypes)
                {
                    types.Add(await Context.Types.FirstOrDefaultAsync(e=>e.Id==brandType.Type.Id));
                }

                brand.Types = types;

            }
            return res;
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
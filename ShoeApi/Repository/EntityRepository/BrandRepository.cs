using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;
namespace WebApplication.Repository.EntityRepository
{
    internal class BrandRepository: BaseRepository<Brand>, IBrandRepository
    {   
        public BrandRepository(ShopContext context)
        {
            Context = context;
        }

        public override async Task<List<Brand>> GetAll()
        {
            var res = await Context.Brands.Include(e=>e.Goods).ToListAsync();
            List<BrandType> brandTypes = await Context.BrandTypes.Include(e => e.Brand).Include(e=>e.Type).ToListAsync();
            foreach (var brand in res)
            {
                foreach (var brandGood in brand.Goods)
                {
                    brandGood.Type.Goods = null;
                }
                List<Type> types = new List<Type>();
                foreach (var bt in brandTypes)
                {
                    if (bt.BrandId == brand.Id)
                    {
                        types.Add(bt.Type);
                    }
                }
                brand.Types = types;

            }
            return res;
        }

        public override async Task<Brand> GetById(long id)
        {
            await CheckForExistingId(id);
            var res = await Context.Brands.Include(e => e.Goods).FirstOrDefaultAsync(b=>b.Id==id);
            List<BrandType> brandTypes = await Context.BrandTypes.Where(bt=>bt.BrandId==id).Include(e => e.Brand).Include(e=>e.Type).ToListAsync();
            foreach (var brandGood in res.Goods)
            {
                brandGood.Type.Goods = null;
            }
            List<Type> types = new List<Type>();
            foreach (var bt in brandTypes)
            {
                if (bt.BrandId == res.Id)
                {
                    types.Add(bt.Type);
                }
            }
            res.Types = types;
            return res;
        }

        public override async Task<bool> IsAlreadyExists(Brand brand)
        {
            if (await Context.Brands.FirstOrDefaultAsync(b => b.Name == brand.Name) == null)
            {
                return false;
            }

            return true;
        }
    }
}
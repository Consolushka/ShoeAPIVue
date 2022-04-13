using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;
using Type = WebApplication.Data.Models.Type;

namespace WebApplication.Repository.EntityRepository
{
    public class BrandTypeRepository: BaseRepository<BrandType>, IBrandTypeRepository
    {
        public BrandTypeRepository(ShopContext context)
        {
            Context = context;
        }

        public override async Task<bool> IsAlreadyExists(BrandType brandType)
        {
            if (await Context.BrandTypes.FirstOrDefaultAsync(bt =>
                bt.Brand.Id == brandType.Brand.Id && bt.Type.Id == brandType.Type.Id) == null)
            {
                return false;
            }

            return true;
        }

        public override async Task<List<BrandType>> GetAll()
        {
            return await Context.BrandTypes.Include(t=>t.Type).Include(t=>t.Brand).ToListAsync();
        }

        public async Task<List<Type>> GetByBrand(long id)
        {
            var res = new List<Type>();
            var brandTypes = await Context.BrandTypes.Where(bt => bt.Brand.Id == id).Include(t=>t.Type).ToListAsync();
            foreach (var brandType in brandTypes)
            {
                res.Add(await Context.Types.Where(t=>t.Id == brandType.Type.Id).FirstOrDefaultAsync());
            }
            return res;
        }

        public async Task<List<Brand>> GetByType(long typeId)
        {
            var res = new List<Brand>();
            var brandTypes = await Context.BrandTypes.Where(bt => bt.Type.Id == typeId).Include(t=>t.Brand).ToListAsync();
            foreach (var brandType in brandTypes)
            {
                res.Add(await Context.Brands.Where(t=>t.Id == brandType.Brand.Id).FirstOrDefaultAsync());
            }
            return res;
        }

        public override async Task<BrandType> GetById(long id)
        {
            var res = await Context.BrandTypes.Include(s=>s.Brand).Include(s=>s.Type).FirstOrDefaultAsync(t => t.Id == id); 
            if (res == null)
                throw new Exception($"Cannot find BrandType with id: {id}");
            return res;
        }
    }
}
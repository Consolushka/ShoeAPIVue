using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;

namespace WebApplication.Repository.EntityRepository
{
    public class BrandTypeRepository: BaseRepository<BrandType>, IBrandTypeRepository
    {
        public BrandTypeRepository(ShopContext context)
        {
            Context = context;
        }

        public override async Task<bool> IsExists(BrandType brandType)
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
            return await Context.BrandTypes.Include(s=>s.Brand).Include(s=>s.Type).ToListAsync();;
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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;

namespace WebApplication.Repository.EntityRepository
{
    internal class GoodsRepository : BaseRepository<Good>, IGoodsRepository
    {

        public GoodsRepository(ShopContext context)
        {
            Context = context;
        }

        public override async Task<bool> IsAlreadyExists(Good good)
        {
            if (await Context.Goods.FirstOrDefaultAsync(g =>
                g.Name == good.Name && g.BrandId == good.BrandId && g.TypeId == good.TypeId) == null)
            {
                return false;
            }

            return true;
        }

        public override async Task<List<Good>> GetAll()
        {
            return await Context.Goods.Include(s=>s.Brand).ToListAsync();
        }

        public override async Task<Good> GetById(long id)
        {
            var res = await Context.Goods.Include(s=>s.Brand).Include(s=>s.Type).FirstOrDefaultAsync(t => t.Id == id); 
            if (res == null)
                throw new Exception($"Cannot find Good with id: {id}");
            return res;
        }
    }
}
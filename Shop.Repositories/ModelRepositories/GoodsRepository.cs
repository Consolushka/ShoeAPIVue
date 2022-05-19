using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.DataBase;
using Microsoft.EntityFrameworkCore;
using Shop.Repositories.Basis;
using Shop.Repositories.Contracts;

namespace Shop.Repositories.ModelRepositories
{
    internal class GoodsRepository : BaseRepository<Good>, IGoodsRepository
    {

        public GoodsRepository(ShopContext context) : base(context)
        {
        }

        public async Task<Good> GetSameGood(Good good)
        {
            return await Context.Goods.FirstOrDefaultAsync(g =>
                g.Name == good.Name && g.BrandId == good.BrandId && g.TypeId == good.TypeId);
        }

        public override async Task<List<Good>> GetAll()
        {
            return await Context.Goods.Include(s=>s.Brand).ToListAsync();
        }

        public override async Task<Good> GetById(long id)
        {
            return await Context.Goods.Include(s=>s.Brand).Include(s=>s.Type).FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
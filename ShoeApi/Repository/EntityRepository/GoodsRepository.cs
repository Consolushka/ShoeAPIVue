using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;

namespace WebApplication.Repository.EntityRepository
{
    public class GoodsRepository : BaseModelProductRepository<Good>, IGoodsRepository
    {

        public GoodsRepository(ShopContext context)
        {
            Context = context;
        }

        public override async Task<List<Good>> GetAll()
        {
            return await Context.Goods.Include(s=>s.Brand).ToListAsync();
        }

        public override async Task<Good> GetById(long id)
        {
            var res = await Context.Goods.Include(s=>s.Brand).FirstOrDefaultAsync(t => t.Id == id); 
            if (res == null)
                throw new Exception($"Cannot find Good with id: {id}");
            return res;
        }
    }
}
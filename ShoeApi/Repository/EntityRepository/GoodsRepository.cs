using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;

namespace WebApplication.Repository.EntityRepository
{
    public class GoodsRepository : BaseRepository<Good>, IGoodsRepository
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
            CheckForId(id);
            return await Context.Goods.Include(s=>s.Brand).FirstOrDefaultAsync(s=>s.Id == id);
        }
    }
}
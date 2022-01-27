using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;

namespace WebApplication.Repository.EntityRepository
{
    public class ShoeRepository : BaseRepository<Shoe>, IShoeRepository
    {

        public ShoeRepository(ShoeContext context)
        {
            Context = context;
        }

        public override async Task<List<Shoe>> GetAll()
        {
            return await Context.Shoes.Include(s=>s.Brand).ToListAsync();
        }

        public override async Task<Shoe> GetById(long id)
        {
            CheckForId(id);
            return await Context.Shoes.Include(s=>s.Brand).FirstOrDefaultAsync(s=>s.Id == id);
        }
    }
}
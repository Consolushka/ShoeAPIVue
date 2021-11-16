using System.Collections.Generic;
using System.Linq;
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

        public new List<Shoe> GetAll()
        {
            return Context.Shoe.Include(s=>s.Brand).ToList();
        }
    }
}
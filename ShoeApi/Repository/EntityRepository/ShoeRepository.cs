using System;
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

        public override List<Shoe> GetAll()
        {
            return Context.Shoes.Include(s=>s.Brand).ToList();
        }

        public override Shoe GetById(long id)
        {
            CheckForId(id);
            return Context.Shoes.Include(s=>s.Brand).FirstOrDefault(s=>s.Id == id);
        }
    }
}
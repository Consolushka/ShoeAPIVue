using System.Collections.Generic;
using System.Linq;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository.EntityRepository
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

        public Shoe Update(Shoe shoe)
        {
            var res = Context.Shoe.Update(shoe);
            Context.SaveChanges();
            return res.Entity;
        }

        public void Delete(long id)
        {
            var shoe = GetById(id);
            Context.Shoe.Remove(shoe);
            Context.SaveChanges();
        }
    }
}
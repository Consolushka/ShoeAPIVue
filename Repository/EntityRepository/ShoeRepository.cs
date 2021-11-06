using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository.EntityRepository
{
    public class ShoeRepository: IShoeRepository
    {
        private readonly ShoeContext _context;

        public ShoeRepository(ShoeContext context)
        {
            _context = context;
        }

        public List<Shoe> GetAll()
        {
            var res =_context.Shoe.Include(s=>s.Brand).ToList();
            return res;
        }

        public Shoe GetById(long id)
        {
            return _context.Shoe.FirstOrDefault(s => s.Id == id);
        }

        public async Task<Shoe> Add(Shoe shoe)
        {
            var res = await _context.AddAsync(shoe);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public Shoe Update(Shoe shoe)
        {
            var res = _context.Shoe.Update(shoe);
            _context.SaveChanges();
            return res.Entity;
        }

        public void Delete(long id)
        {
            var shoe = GetById(id);
            _context.Shoe.Remove(shoe);
            _context.SaveChanges();
        }
    }
}
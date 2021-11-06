using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Repository.Contracts;

namespace Repository.EntityRepository
{
    public class BrandRepository: IBrandRepository
    {
        private readonly ShoeContext _context;

        public BrandRepository(ShoeContext context)
        {
            _context = context;
        }

        public List<Brand> GetAll()
        {
            return _context.Brand.ToList();
        }

        public Brand GetById(long id)
        {
            return _context.Brand.FirstOrDefault(b => b.Id == id);
        }

        public async Task<Brand> Add(Brand brand)
        {
            var res = await _context.AddAsync(brand);
            _context.SaveChanges();
            return res.Entity;
        }

        public Brand Update(Brand brand)
        {
            var res = _context.Brand.Update(brand);
            _context.SaveChanges();
            return res.Entity;
        }

        public void Delete(long id)
        {
            var brand = GetById(id);
            _context.Brand.Remove(brand);
            _context.SaveChanges();
        }
    }
}
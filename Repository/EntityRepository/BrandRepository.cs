﻿using System.Collections.Generic;
using System.Linq;
using Entities.Models;

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

        public Brand GetById(long Id)
        {
            return _context.Brand.FirstOrDefault(b => b.Id == Id);
        }

        public long Add(Brand brand)
        {
            var res = _context.Add(brand);
            _context.SaveChanges();
            return res.Entity.Id;
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
﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

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
            return _context.Shoe.ToList();
        }

        public Shoe GetById(long id)
        {
            return _context.Shoe.FirstOrDefault(s => s.Id == id);
        }

        public async Task<long> Add(Shoe shoe)
        {
            var res = await _context.AddAsync(shoe);
            await _context.SaveChangesAsync();
            return res.Entity.Id;
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
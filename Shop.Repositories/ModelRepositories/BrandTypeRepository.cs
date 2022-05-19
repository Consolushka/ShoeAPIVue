using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.DataBase;
using Microsoft.EntityFrameworkCore;
using Shop.Repositories.Basis;
using Shop.Repositories.Contracts;
using Type = Shop.Data.Models.Type;

namespace Shop.Repositories.ModelRepositories
{
    internal class BrandTypeRepository: BaseRepository<BrandType>, IBrandTypeRepository
    {
        public BrandTypeRepository(ShopContext context) : base(context)
        {
        }

        public override async Task<bool> IsAlreadyExists(BrandType brandType)
        {
            if (await Context.BrandTypes.FirstOrDefaultAsync(bt =>
                bt.BrandId == brandType.BrandId && bt.TypeId == brandType.TypeId) == null)
            {
                return false;
            }

            return true;
        }

        public override async Task<BrandType> Add(BrandType entity)
        {
            if (await IsAlreadyExists(entity))
            {
                throw new Exception("Same BrandType already exists");
            }

            entity.Brand = await Context.Brands.FindAsync(entity.BrandId);
            entity.Type = await Context.Types.FindAsync(entity.TypeId);
            
            var res = await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return res.Entity;
        }

        public override async Task<List<BrandType>> GetAll()
        {
            return await Context.BrandTypes.Include(t=>t.Type).Include(t=>t.Brand).ToListAsync();
        }

        public async Task<List<Type>> GetByBrand(long id)
        {
            var res = new List<Type>();
            if (await Context.Brands.FindAsync(id) == null)
            {
                throw new Exception("Cannot find this Brand");
            }
            var brandTypes = await Context.BrandTypes.Where(bt => bt.Brand.Id == id).Include(t=>t.Type).ToListAsync();
            foreach (var brandType in brandTypes)
            {
                res.Add(await Context.Types.Where(t=>t.Id == brandType.Type.Id).FirstOrDefaultAsync());
            }
            return res;
        }

        public async Task<List<Brand>> GetByType(long typeId)
        {
            var res = new List<Brand>();
            if (await Context.Types.FindAsync(typeId) == null)
            {
                throw new Exception("Cannot find this Type");
            }
            var brandTypes = await Context.BrandTypes.Where(bt => bt.Type.Id == typeId).Include(t=>t.Brand).ToListAsync();
            foreach (var brandType in brandTypes)
            {
                res.Add(await Context.Brands.Where(t=>t.Id == brandType.Brand.Id).FirstOrDefaultAsync());
            }
            return res;
        }

        public override async Task<BrandType> GetById(long id)
        {
            var res = await Context.BrandTypes.Include(s=>s.Brand).Include(s=>s.Type).FirstOrDefaultAsync(t => t.Id == id); 
            if (res == null)
                throw new Exception($"Cannot find BrandType with id: {id}");
            return res;
        }
    }
}
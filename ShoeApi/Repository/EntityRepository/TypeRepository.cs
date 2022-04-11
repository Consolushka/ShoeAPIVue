using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;

namespace WebApplication.Repository.EntityRepository
{
    public class TypeRepository: BaseModelProductRepository<Type>, ITypeRepository
    {
        public TypeRepository(ShopContext context)
        {
            Context = context;
        }
        //
        // public override async Task<List<Type>> GetAll()
        // {
        //     List<Type> res = await Context.Types.ToListAsync();
        //     List<BrandType> brandTypes = await Context.BrandTypes.Include(bt=>bt.Type).Include(bt=>bt.Brand).ToListAsync();
        //     return res;
        // }
        //
        // public override async Task<Type> GetById(long id)
        // {
        //     Type res = await Context.Types.FindAsync(id);
        //     List<BrandType> brandTypes = await Context.BrandTypes.Include(bt=>bt.Type).Include(bt=>bt.Brand).ToListAsync();
        //     return res;
        // }
    }
}
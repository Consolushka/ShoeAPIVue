using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.DataBase;
using Microsoft.EntityFrameworkCore;
using Shop.Repositories.Basis;
using Shop.Repositories.Contracts;
using Type = Shop.Data.Models.Type;

namespace Shop.Repositories.ModelRepositories
{
    internal class TypeRepository: BaseRepository<Type>, ITypeRepository
    {
        public TypeRepository(ShopContext context) : base(context)
        {
        }

        public async Task<Type> GetSameType(Type type)
        {
            return await Context.Types.FirstOrDefaultAsync(t => t.Name == type.Name);
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
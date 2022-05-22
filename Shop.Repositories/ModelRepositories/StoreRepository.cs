using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Models;
using Shop.DataBase;
using Shop.Repositories.Basis;
using Shop.Repositories.Contracts;

namespace Shop.Repositories.ModelRepositories
{
    internal class StoreRepository: BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(ShopContext context) : base(context)
        {
        }

        public async Task<List<Store>> GetByAvailableGood(long goodId)
        {
            return await Context.Stores.ToListAsync();
        }
    }
}
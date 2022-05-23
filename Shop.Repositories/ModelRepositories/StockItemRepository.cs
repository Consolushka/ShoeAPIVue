using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Models;
using Shop.DataBase;
using Shop.Repositories.Basis;
using Shop.Repositories.Contracts;

namespace Shop.Repositories.ModelRepositories
{
    internal class StockItemRepository: BaseRepository<StockItem>, IStockItemRepository
    {
        public StockItemRepository(ShopContext context) : base(context)
        {
        }

        public async Task<StockItem> GetByGoodAndStore(long goodId, long storeId)
        {
            return await Context.StockItems.FirstOrDefaultAsync(si => si.GoodId == goodId && si.StoreId == storeId);
        }
    }
}
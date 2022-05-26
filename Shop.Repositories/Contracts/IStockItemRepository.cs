using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Basis;

namespace Shop.Repositories.Contracts
{
    public interface IStockItemRepository: IGettable<StockItem>, IAddable<StockItem>, IUpdatable<StockItem>
    {
        Task<StockItem> GetByGoodAndStore(long goodId, long storeId);
    }
}
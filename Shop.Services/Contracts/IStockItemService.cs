using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;

namespace Shop.Services.Contracts
{
    public interface IStockItemService
    {
        Task<List<StockItem>> GetAll();
        Task<StockItem> GetById(long id);
        Task<StockItem> Add(StockItem stockItem);
        Task WriteOff(long stockItemId, int minusCount);
        Task Refill(long stockItemId, int plusCount);
    }
}
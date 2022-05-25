using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Contracts;
using Shop.Services.Contracts;

namespace Shop.Services.ModelServices
{
    public class StockItemService: IStockItemService
    {
        private readonly IStockItemRepository _repository;

        public StockItemService(IStockItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<StockItem>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<StockItem> Add(StockItem stockItem)
        {
            var curr = await _repository.GetByGoodAndStore(stockItem.GoodId, stockItem.StoreId);
            if (curr != null)
            {
                throw new Exception("Same StockItem with pair of good and store already exists");
            }

            return await _repository.Add(stockItem);
        }

        public async Task WriteOff(long stockItemId, int minusCount)
        {
            var curr = await _repository.GetById(stockItemId);
            if (curr != null)
            {
                throw new Exception("Same StockItem with pair of good and store already exists");
            }
            curr.Count -= minusCount;
            await _repository.Update(curr);
        }

        public async Task Refill(long stockItemId, int plusCount)
        {
            var curr = await _repository.GetById(stockItemId);
            if (curr != null)
            {
                throw new Exception("Same StockItem with pair of good and store already exists");
            }
            curr.Count += plusCount;
            await _repository.Update(curr);
        }
    }
}
using System;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Contracts;
using Shop.Services.Contracts;

namespace Shop.Services.ModelServices
{
    internal class BasketItemService: IBasketItemService
    {
        private readonly IBasketItemRepository _repository;

        public BasketItemService(IBasketItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<BasketItem> Add(BasketItem basketItem)
        {
            return await _repository.Add(basketItem);
        }

        public async Task UpdateQuantity(long id, int newCount)
        {
            var current = await _repository.GetById(id);
            if (current == null)
            {
                throw new Exception($"Cannot find BasketItem with id: {id}");
            }
            current.Count = newCount;
            await _repository.Update(current);
        }

        public async Task Delete(long id)
        {
            try
            {
                await _repository.Delete(id);
            }
            catch(System.NullReferenceException ex)
            {
                throw new Exception($"Cannot find BasketItem with id: {id}");
            }
        }
    }
}
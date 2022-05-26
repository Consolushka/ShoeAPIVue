using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;

namespace Shop.Services.Contracts
{
    public interface IBasketItemService
    {
        Task<BasketItem> Add(BasketItem basketItem);
        Task UpdateQuantity(long id, int newCount);
        Task Delete(long id);
    }
}
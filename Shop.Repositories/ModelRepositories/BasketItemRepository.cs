using Shop.Data.Models;
using Shop.DataBase;
using Shop.Repositories.Basis;
using Shop.Repositories.Contracts;

namespace Shop.Repositories.ModelRepositories
{
    internal class BasketItemRepository: BaseRepository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(ShopContext context) : base(context)
        {
            
        }
    }
}
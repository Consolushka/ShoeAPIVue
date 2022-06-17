using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Models;
using Shop.DataBase;
using Shop.Repositories.Contracts;

namespace Shop.Repositories.ModelRepositories
{
    internal class BasketRepository: IBasketRepository
    {
        private readonly ShopContext _context;

        public BasketRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<Basket> GetByUser(long id)
        {
            var res = await _context.Baskets.Include(b=>b.BasketItems).FirstOrDefaultAsync(b => b.UserId == id);
            if (res == null)
            {
                return res;
            }
            foreach (var item in res.BasketItems)
            {
                item.StockItem = _context.StockItems.Include(s=>s.Good).FirstOrDefault(s=>s.Id==item.StockItemId);
            }

            return res;
        }

        public async Task<Basket> Add(Basket basket)
        {
            var res = await _context.Baskets.AddAsync(basket);
            await _context.SaveChangesAsync();
            return res.Entity;
        }
    }
}
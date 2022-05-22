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
            return await _context.Baskets.FirstOrDefaultAsync(b => b.UserId == id);
        }

        public async Task<Basket> Add(Basket basket)
        {
            var res = await _context.Baskets.AddAsync(basket);
            await _context.SaveChangesAsync();
            return res.Entity;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Models;
using Shop.DataBase;
using Shop.Repositories.Basis;
using Shop.Repositories.Contracts;

namespace Shop.Repositories.ModelRepositories
{
    internal class OrderItemRepository: BaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ShopContext Context) : base(Context)
        {
            
        }

        public async Task<List<OrderItem>> GetAllByOrder(long orderId)
        {
            return await Context.OrderItems.Where(oi => oi.OrderId == orderId).ToListAsync();
        }
    }
}
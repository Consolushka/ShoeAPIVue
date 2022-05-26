using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Basis;

namespace Shop.Repositories.Contracts
{
    public interface IOrderItemRepository: IGettable<OrderItem>, IAddable<OrderItem>, IUpdatable<OrderItem>
    {
        Task<List<OrderItem>> GetAllByOrder(long orderId);
    }
}
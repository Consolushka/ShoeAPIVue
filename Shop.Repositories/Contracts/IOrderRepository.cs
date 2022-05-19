using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Basis;

namespace Shop.Repositories.Contracts
{
    public interface IOrderRepository: IGettable<Order>, IAddable<Order>
    {
        Task<List<Order>> GetAllByUser(long userId);
        
        Task UpdateStatus(long id, short status);
        Task ChangePaidTrigger(long id, bool isPaid);
    }
}
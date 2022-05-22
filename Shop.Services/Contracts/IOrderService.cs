using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Data.ViewModels;

namespace Shop.Services.Contracts
{
    public interface IOrderService
    {
        Task<List<Order>> GetAll();
        Task<Order> GetById(long id);
        Task<List<Order>> GetByUser(long id);
        Task<Order> Add(Order order);
        Task UpdateStatus(short status, long id);
        Task ChangePaidTrigger(bool isPaid, long id);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Contracts;
using Shop.Services.Contracts;

namespace Shop.Services.ModelServices
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Order> GetById(long id)
        {
            // await _repository.CheckForExistingId(id);
            return await _repository.GetById(id);
        }

        public async Task<Order> Add(Order order)
        {
            if (order.IsNull())
            {
                throw new Exception("Model is Null");
            }
            return await _repository.Add(order);
        }

        public async Task UpdateStatus(short status, long id)
        {
            // await _repository.CheckForExistingId(id);
            await _repository.UpdateStatus(id, status);
        }

        public async Task ChangePaidTrigger(bool isPaid, long id)
        {
            // await _repository.CheckForExistingId(id);
            await _repository.ChangePaidTrigger(id, isPaid);
        }
    }
}
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
            
            var res =await _repository.GetById(id);
            if (res == null)
            {
                throw new System.NullReferenceException($"Cannot find Order with id: {id}");
            }
            return res;
        }

        public async Task<List<Order>> GetByUser(long id)
        {
            return await _repository.GetAllByUser(id);
        }

        public async Task<Order> Add(Order order)
        {
            return await _repository.Add(order);
        }

        public async Task UpdateStatus(short status, long id)
        {
            try
            {
                await _repository.GetById(id);
                await _repository.UpdateStatus(id, status);
            }
            catch(System.NullReferenceException ex)
            {
                throw new System.NullReferenceException($"Cannot find Order with id: {id}");
            }
        }

        public async Task ChangePaidTrigger(bool isPaid, long id)
        {
            try
            {
                await _repository.GetById(id);
                await _repository.ChangePaidTrigger(id, isPaid);
            }
            catch(System.NullReferenceException ex)
            {
                throw new System.NullReferenceException($"Cannot find order with id: {id}");
            }
        }
    }
}
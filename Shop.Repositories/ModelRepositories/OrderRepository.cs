using System;
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
    public class OrderRepository: IOrderRepository
    {
        private ShopContext Context;
        
        public OrderRepository(ShopContext context)
        {
            Context = context;
        }

        public async Task<List<Order>> GetAll()
        {
            var res = await Context.Orders.Include(o => o.User).Include(o => o.OrderItems).ToListAsync();
            foreach (var order in res)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    orderItem.StockItem = Context.StockItems.Find(orderItem.StockItemId);
                    orderItem.StockItem.Good = Context.Goods.Find(orderItem.StockItem.GoodId);
                }
            }
            return res;
        }

        public async Task<Order> GetById(long id)
        {
            return await Context.Orders.Include(o => o.User).Include(o => o.OrderItems).FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<Order> Add(Order order)
        {
            var res = await Context.AddAsync(order);
            await Context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<List<Order>> GetAllByUser(long userId)
        {
            return await Context.Orders.Where(o => o.UserId == userId).Include(o => o.OrderItems).ToListAsync();
        }

        public async Task UpdateStatus(long id, short status)
        {
            var order = await Context.Orders.FindAsync(id);
            order.Status = status;
            Context.Update(order);
        }

        public async Task ChangePaidTrigger(long id, bool isPaid)
        {
            var order = await Context.Orders.FindAsync(id);
            order.IsPaid = isPaid;
            Context.Update(order);
        }
    }
}
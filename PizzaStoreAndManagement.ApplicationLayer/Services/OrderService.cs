using Microsoft.EntityFrameworkCore;
using PizzaStoreAndManagement.DomainLayer.Models;
using PizzaStoreAndManagement.Persistance.Context;
using PizzaStoreAndManagement.Persistance.Interfaces;
namespace PizzaStoreAndManagement.ApplicationLayer.Services
{
    public class OrderService : IOrderRepository
    {
        private readonly PizzaStoreDBContext _dbContext;
        public OrderService(PizzaStoreDBContext db)
        {
            _dbContext = db;
        }
        public async Task<Order> CreateOrder(Guid UserID, DateTime created)
        {
            var order = new Order(UserID, created);
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }
        public async Task<Order> UpdateStatusOrder(Guid OrderId, string Status)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == OrderId);
            if(order == null)
            {
                return order;
            }
            order.Status = Status;
            await _dbContext.SaveChangesAsync();
            return order;
        }
        public async Task<Order> UpdateOrder(Guid OrderId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == OrderId);
            var orders = await _dbContext.OrderItems.Where(x => x.OrderId == OrderId).ToListAsync();
            if(order == null)
            {
                return order;
            }
            order.AddItems(orders);
            await _dbContext.SaveChangesAsync();
            return order;
        }
        public async Task<Order> GetOrderByID(Guid OrderId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id ==  OrderId);
            return order;
        }
        public async Task<Order> GetOrderByUserID(Guid UserId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.UserId == UserId);
            return order;
        }
        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _dbContext.Orders.Where(x => x.Status != "Выполнен").Include(x => x.User).ToListAsync();
            return orders;
        }
        public async Task<bool> DeleteOrder(Guid OrderId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == OrderId);
            if(order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PizzaStoreAndManagement.DomainLayer.Models;
using PizzaStoreAndManagement.Persistance.Context;
using PizzaStoreAndManagement.Persistance.Interfaces;

namespace PizzaStoreAndManagement.ApplicationLayer.Services
{
    public class OrderItemService : IOrderItemRepository
    {
        private readonly PizzaStoreDBContext _dbContext;
        public OrderItemService(PizzaStoreDBContext db)
        {
            _dbContext = db;           
        }
        public async Task<OrderItem> CreateOrderItem(Guid FoodID, int amount, Guid OrderID, decimal Price, string? Extra, string Name) 
        {
            var or = await _dbContext.OrderItems.FirstOrDefaultAsync(x => x.FoodId == FoodID);
            if(or != null)
            {
                var item = await UpdateOrderItem(or.Id, amount);
                return item;
            }
            else
            {
                var oritem = new OrderItem(OrderID, FoodID, amount, Price, Extra, Name);
                await _dbContext.OrderItems.AddAsync(oritem);
                await _dbContext.SaveChangesAsync();
                return oritem;
            }
        }
        public async Task<OrderItem> UpdateOrderItem(Guid OrderItemID, int diff)
        {
            var oritem = await _dbContext.OrderItems.FirstOrDefaultAsync(x => x.Id == OrderItemID);
            if (oritem == null)
            {
                return null;
            }
            oritem.Quantity += diff;
            await _dbContext.SaveChangesAsync();
            return oritem;
        }
        public async Task<bool> DeleteOrderItem(Guid OrderItemID) 
        {
            var oritem = await _dbContext.OrderItems.FirstOrDefaultAsync(x => x.Id == OrderItemID);
            if(oritem == null)
            {
                return false;
            }
            _dbContext.Remove(oritem);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<OrderItem> GetOrderItemByID(Guid OrderItemID)
        {
            var oritem = await _dbContext.OrderItems.FirstOrDefaultAsync(x => x.Id == OrderItemID);
            return oritem;
        }
        public async Task<List<OrderItem>> GetAllOrdersItemOfOrderById(Guid OrderID)
        {
            var oritems = await _dbContext.OrderItems.Where(x => x.OrderId == OrderID).ToListAsync();
            return oritems;
        }
    }
}

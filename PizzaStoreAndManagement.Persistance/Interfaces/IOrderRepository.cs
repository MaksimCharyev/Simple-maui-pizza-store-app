using PizzaStoreAndManagement.DomainLayer.Models;

namespace PizzaStoreAndManagement.Persistance.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(Guid UserId, DateTime Created);
        Task<Order> UpdateOrder(Guid OrderId);
        Task<Order> UpdateStatusOrder(Guid OrderId, string Status);
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderByID(Guid OrderId);
        Task<Order> GetOrderByUserID(Guid UserId);
        Task<bool> DeleteOrder(Guid OrderID);
    }
}

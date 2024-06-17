using PizzaStoreAndManagement.DomainLayer.Models;

namespace PizzaStoreAndManagement.Persistance.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> CreateOrderItem(Guid FoodID, int amount, Guid OrderID, decimal Price, string? Extra, string Name);
        Task<bool> DeleteOrderItem(Guid OrderItemID);
        Task<OrderItem> GetOrderItemByID(Guid OrderItemID);
        Task<List<OrderItem>> GetAllOrdersItemOfOrderById(Guid OrderID);
    }
}

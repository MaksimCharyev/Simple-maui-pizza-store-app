using PizzaStoreAndManagement.DomainLayer.Models;

namespace PizzaStoreAndManagement.Persistance.Interfaces
{
    public interface ICartItemRepository
    {
        Task<CartItem> CreateCartItem(Guid CartId, Guid FoodID, int Quantity, string? Extra, int mod, decimal price);
        Task<CartItem> UpdateCartItem(Guid CartItemId, Guid FoodItemId, int Change);
        Task<bool> DeleteCartItem(Guid CartId, Guid CartItemId);
        Task<bool> DeleteCartItems(Guid CartId);
        Task<CartItem> GetCartItemByID(Guid CartItemId);
        Task<CartItem> GetCartItemByCartID(Guid CartId);
        Task<List<CartItem>> GetAllCartItemsByCartID(Guid CartId);
    }
}


using Microsoft.EntityFrameworkCore;
using PizzaStoreAndManagement.DomainLayer.Models;
using PizzaStoreAndManagement.Persistance.Context;
using PizzaStoreAndManagement.Persistance.Interfaces;

namespace PizzaStoreAndManagement.ApplicationLayer.Services
{
    public class CartItemService : ICartItemRepository
    {
        private readonly PizzaStoreDBContext _dbContext;
        public CartItemService(PizzaStoreDBContext db)
        {
            _dbContext = db;
        }
        public async Task<bool> DeleteCartItems(Guid CartId)
        {
            var cart = await _dbContext.Carts.FirstOrDefaultAsync(x => x.Id == CartId);
            var cartItems = await _dbContext.CartItems.Where(x => x.CartId == CartId).ToListAsync();
            _dbContext.RemoveRange(cartItems);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<CartItem> CreateCartItem(Guid CartId, Guid FoodID, int Quantity, string? Extra, int mod, decimal price)
        {
            var cartItem = new CartItem(CartId, FoodID, Quantity, Extra, mod, price);
            await _dbContext.CartItems.AddAsync(cartItem);
            await _dbContext.SaveChangesAsync();
            return cartItem;
        }

        public async Task<bool> DeleteCartItem(Guid CartId, Guid CartItemId)
        {
            var cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(x => x.Id == CartItemId);
            if (cartItem != null)
            {
                _dbContext.CartItems.Remove(cartItem);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<CartItem>> GetAllCartItemsByCartID(Guid CartId)
        {
            List<CartItem> cartItems = await _dbContext.CartItems.Where(x => x.CartId == CartId).ToListAsync();
            return cartItems;
        }

        public async Task<CartItem> GetCartItemByCartID(Guid CartId)
        {
            var cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(x => x.CartId == CartId);
            return cartItem;
        }

        public async Task<CartItem> GetCartItemByID(Guid CartItemId)
        {
            var cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(x => x.Id == CartItemId);
            return cartItem;
        }

        public async Task<CartItem> UpdateCartItem(Guid CartItemId, Guid FoodItemId, int Change)
        {
            var cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(x => x.Id == CartItemId);
            cartItem.Quantity = Change;
            await _dbContext.SaveChangesAsync();
            return cartItem;
        }
    }
}

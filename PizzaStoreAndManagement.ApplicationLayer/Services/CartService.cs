using Microsoft.EntityFrameworkCore;
using PizzaStoreAndManagement.DomainLayer.Models;
using PizzaStoreAndManagement.Persistance.Context;
using PizzaStoreAndManagement.Persistance.Interfaces;
namespace PizzaStoreAndManagement.ApplicationLayer.Services
{
    public class CartService : ICartRepository
    {
        private readonly PizzaStoreDBContext _dbcontext;
        public CartService(PizzaStoreDBContext db)
        {
            _dbcontext = db;
        }
        public async Task<Cart> CreateCart(Guid UserId)
        {
            var cart = new Cart(UserId);
            _dbcontext.Carts.Add(cart);
            await _dbcontext.SaveChangesAsync();
            return cart;
        }

        public async Task<bool> DeleteCart(Guid CartId)
        {
            var cart = await _dbcontext.Carts.FirstOrDefaultAsync(x=> x.Id == CartId);
            if (cart != null)
            {
                _dbcontext.Carts.Remove(cart);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Cart> GetCartByID(Guid CartId)
        {
            var cart = await _dbcontext.Carts.FirstOrDefaultAsync(x => x.Id == CartId);
            return cart;
        }

        public async Task<Cart> GetCartByUserID(Guid UserId)
        {
            var cart = await _dbcontext.Carts.FirstOrDefaultAsync(x => x.UserId == UserId);
            return cart;
        }
    }
}

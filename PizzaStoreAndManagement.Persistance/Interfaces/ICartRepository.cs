using PizzaStoreAndManagement.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStoreAndManagement.Persistance.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> CreateCart(Guid UserId);
        Task<Cart> GetCartByID(Guid CartId);
        Task<Cart> GetCartByUserID(Guid UserId);
        Task<bool> DeleteCart(Guid OrderID);
    }
}

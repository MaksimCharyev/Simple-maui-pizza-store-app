using PizzaStoreAndManagement.DomainLayer.Models;
namespace PizzaStoreAndManagement.Persistance.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUser(string Name, string Email, string Password);
        Task<bool> VerifyUser(string Email, string Password);
        Task<User> GetUserByID(Guid guid);
        Task<bool> VerifyAdmin(string Email, string Password);
    }
}

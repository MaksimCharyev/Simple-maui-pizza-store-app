using PizzaStoreAndManagement.DomainLayer.Models;
namespace PizzaStoreAndManagement.Persistance.Interfaces
{
    public interface IFoodRepository
    {
        Task<Food> CreateFood(string Name, string Description);
        Task<Food> GetFoodByID(Guid guid);
        Task<List<Food>> GetAllFoods();
        Task<Food> UpdateFood(Guid foodId, string Name, string Description);
    }
}

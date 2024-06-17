using Microsoft.EntityFrameworkCore;
using PizzaStoreAndManagement.DomainLayer.Models;
using PizzaStoreAndManagement.Persistance.Context;
using PizzaStoreAndManagement.Persistance.Interfaces;
namespace PizzaStoreAndManagement.ApplicationLayer.Services
{
    internal class FoodService : IFoodRepository
    {
        private readonly PizzaStoreDBContext _dbContext;
        public FoodService(PizzaStoreDBContext db)
        {
            _dbContext = db;
        }
        public async Task<Food> CreateFood(string Name, string Description)
        {
            var food = new Food(Name, Description);
            await _dbContext.Foods.AddAsync(food);
            await _dbContext.SaveChangesAsync();
            return food;
        }
        public async Task<Food> GetFoodByID(Guid id)
        {
            var food = await _dbContext.Foods.FirstOrDefaultAsync(f => f.Id == id);
            return food;
        }
        public async Task<Food> UpdateFood(Guid id, string? Name, string? Description)
        {
            var food = await _dbContext.Foods.FirstOrDefaultAsync(f => f.Id == id);
            if (Name != null && food != null)
            {
                food.Name = Name;
            }
            if(Description != null && food != null)
            {
                food.Description = Description;
            }
            await _dbContext.SaveChangesAsync();
            return food;
        }
        public async Task<List<Food>> GetAllFoods()
        {
            var foods = await _dbContext.Foods.ToListAsync();
            return foods;
        }
    }
}

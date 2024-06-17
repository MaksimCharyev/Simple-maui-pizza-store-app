using PizzaStoreAndManagement.Persistance.Context;
using PizzaStoreAndManagement.Persistance.Interfaces;
using PizzaStoreAndManagement.DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
namespace PizzaStoreAndManagement.ApplicationLayer.Services
{
    internal class UserService : IUserRepository
    {
        private readonly PizzaStoreDBContext _dbContext;
        private readonly IUserToken token;
        public UserService(PizzaStoreDBContext db, IUserToken token)
        {
            _dbContext = db;
            this.token = token;
        }
        public async Task<User> CreateUser(string Name, string Email, string Password)
        {
            var user = new User(Name, Email, Password, false);
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        public async Task<bool> VerifyUser(string Email, string Password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == Email && user.Password == Password);
            if(user != null)
            {
                token.UserId = user.Id;
                return true;
            }
            return false;
        }
        public async Task<bool> VerifyAdmin(string Email, string Password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == Email && user.Password == Password);
            if (user != null)
            {
                if (user.isAdmin)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<User> GetUserByID(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(User => User.Id == id);
            return user;
        }
    }
}

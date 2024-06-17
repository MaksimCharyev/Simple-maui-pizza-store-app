using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStoreAndManagement.ApplicationLayer.Services
{
    public interface IUserToken
    {
        Guid UserId { get; set; }
        void SaveUserId(Guid userId);
        Guid GetUserId();
    }

    public class UserToken : IUserToken
    {
        public Guid UserId { get; set; }

        public void SaveUserId(Guid userId) { 
            UserId = userId;
        }

        public Guid GetUserId()
        {
            return UserId;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityManager.Models;

namespace SmartShop.AccountService.Models
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsers { get; }
        public void CreateUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(User user);
        public User GetUserById(long id);
        public RequestUser GetUser(string name, string password);
        public CurrentUser GetCurrentUser();

    }
}

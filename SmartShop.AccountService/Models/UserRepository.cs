using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdentityManager.Ifrastructure;
using IdentityManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace SmartShop.AccountService.Models
{
    public class UserRepository : IUserRepository
    {
        private AccountDbContext context;
        private IHttpContextAccessor httpContext;
        private ICurrentUserSaver saver;

        public User GetUserById(long id)
        {
            return context.Users.First(u=>u.Id == id);
        }

        public UserRepository(AccountDbContext context, IHttpContextAccessor httpContext, ICurrentUserSaver saver)
        {
            this.saver = saver;
            this.httpContext = httpContext;
            this.context = context;
        }

        public IEnumerable<User> GetUsers => context.Users;

        public void CreateUser(User user)
        {
            user.Role = "User";
            user.Password = PasswordHesher.HeshPassword(user.Password);
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            user.Password = PasswordHesher.HeshPassword(user.Password);
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public RequestUser GetUser(string name, string password)
        {
            User user;
            try
            {
                user = context.Users.First(u => u.Name == name && u.Password == PasswordHesher.HeshPassword(password));

                return new RequestUser { Name = user.Name, Email = user.Email, Role = user.Role };
            }
            catch
            {
                return null;
            }
            
        }

        public void UpdateUser(User user)
        {
            user.Password = PasswordHesher.HeshPassword(user.Password);
            user.Role = "User";
            context.Users.Update(user);
            context.SaveChanges();
        }

        public CurrentUser GetCurrentUser()
        {
            return saver.SetCurrentUser();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityManager.Models;

namespace SmartShop.AccountService.Models
{
    public interface ICurrentUserSaver
    {
        public void GetCurrentUser(CurrentUser currentUser);
        public CurrentUser SetCurrentUser();
    }

    public class CurrentUserSaver : ICurrentUserSaver
    {
        private CurrentUser CurrentUser;

        public void GetCurrentUser(CurrentUser user)
        {
            CurrentUser = user;
        }

        public CurrentUser SetCurrentUser()
        {
            return CurrentUser;
        }
    }
}

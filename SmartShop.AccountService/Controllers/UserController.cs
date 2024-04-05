using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityManager.Models;
using SmartShop.AccountService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace SmartShop.AccountService.Controllers
{
    [ApiController]
    [Route("Account")]
    public class UserController : ControllerBase
    {
        private ICurrentUserSaver saver;
        private IUserRepository UserRepository;
        private IHttpContextAccessor httpContext;

        public UserController(IUserRepository repository, IHttpContextAccessor httpContext, ICurrentUserSaver saver)
        {
            this.saver = saver;
            this.httpContext = httpContext;
            UserRepository = repository;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("GetUserById")]
        public IActionResult GetUserById(long id)
        {
            if (id != default(long))
            {
                User user = UserRepository.GetUserById(id);
                if (user != null)
                {
                    return Ok(user);
                }
            }

            return BadRequest();
        }

        [HttpPost("Create")]
        public IActionResult CreateUser(User user)
        {
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    UserRepository.CreateUser(user);
                    return Ok();
                }
            }

            return BadRequest();
        }

        [Authorize(Roles = "User")]
        [HttpPost("Update")]
        public IActionResult UpdateUser(User user)
        {
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    UserRepository.CreateUser(user);
                    return Ok();
                }
            }

            return BadRequest();
        }

        [Authorize(Roles = "User")]
        [HttpDelete("Delete")]
        public IActionResult DeleteUser(User user)
        {
            UserRepository.DeleteUser(user);
            return Ok();
        }

        [HttpGet("GetUser")]
        public RequestUser GetUser(string name, string password)
        {
            RequestUser user = UserRepository.GetUser(name, password);
            if (user == null)
            {
                return null;
            }
            saver.GetCurrentUser(new CurrentUser { Name = user.Name, Email = user.Email });

            return user;
        }

        //[Authorize(Roles ="Admin")]
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            
            return Ok(UserRepository.GetUsers);
        }

        [HttpGet("CurrentUser")]
        public IActionResult GetCurrentUser()
        {
            return Ok(UserRepository.GetCurrentUser());
        }
    }
}

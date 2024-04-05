using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartShop.AccountService.Models;
using Grpc.Core;

namespace SmartShop.AccountService.ProtoServices
{
    public class AccountServiceProtoModel : AccountServiceProto.AccountServiceProtoBase
    {
        private ILogger<AccountServiceProtoModel> logger;
        private IUserRepository repository;
        private AccountDbContext dbContext;

        public AccountServiceProtoModel(ILogger<AccountServiceProtoModel> logger, IUserRepository repository, AccountDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.repository = repository;
        }

        public override Task<UserModelCreated> CreateUser(UserModel request, ServerCallContext context)
        {
            User user = new User
            {
                Name = request.Name,
                Password = request.Password,
                Address = request.Address,
                Role = request.Role,
                Email = request.Email,
                IsMan = request.IsMan
            };

            repository.CreateUser(user);
            return Task.FromResult(new UserModelCreated());
        }

        public override Task<UserModelUpdated> UpdateUser(UserModel request, ServerCallContext context)
        {
            User user = new User
            {
                Id = request.Id,
                Name = request.Name,
                Password = request.Password,
                Address = request.Address,
                Role = request.Role,
                Email = request.Email,
                IsMan = request.IsMan
            };

            repository.UpdateUser(user);
            return Task.FromResult(new UserModelUpdated());
        }

        public override Task<UserModelDeleted> DeleteUser(UserModel request, ServerCallContext context)
        {
            User user = new User
            {
                Id = request.Id,
                Name = request.Name,
                Password = request.Password,
                Address = request.Address,
                Role = request.Role,
                Email = request.Email,
                IsMan = request.IsMan
            };

            repository.DeleteUser(user);
            return Task.FromResult(new UserModelDeleted());
        }

        public override Task<UserModel> GetUser(UserModelId request, ServerCallContext context)
        {
            User user = repository.GetUserById(request.Id);
            UserModel userModel = new UserModel
            {
                Id = (int)user.Id,
                Name = user.Name,
                Password = user.Password,
                Address = user.Address,
                Role = user.Role,
                Email = user.Email,
                IsMan = user.IsMan
            };

            return Task.FromResult(userModel);
        }

        public override async Task GetUsers(UserModels request, IServerStreamWriter<UserModel> responseStream, ServerCallContext context)
        {
            List<User> users = repository.GetUsers.ToList();
            
            foreach (User user in users)
            {
                await responseStream.WriteAsync(new UserModel 
                {
                    Id = (int)user.Id,
                    Name = user.Name,
                    Password = user.Password,
                    Address = user.Address,
                    Role = user.Role,
                    Email = user.Email,
                    IsMan = user.IsMan
                });

                
            }
        }

        public override Task<UserModelId> GetUsersId(UserModelsEmailAndName request, ServerCallContext context)
        {
            User user = dbContext.Users.First(u => u.Name == request.Name && u.Email == request.Email);

            return Task.FromResult(new UserModelId {Id = (int)user.Id });
        }
    }
}

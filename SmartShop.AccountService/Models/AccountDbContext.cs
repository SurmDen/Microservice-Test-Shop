using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdentityManager.Ifrastructure;

namespace SmartShop.AccountService.Models
{
    public class AccountDbContext: DbContext
    {
        public AccountDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(new User[] { new User 
            {
                Id = 1,
                Name = "Admin",
                Email = "Admin@gmail.com",
                Address = "Los Angeles, USA",
                IsMan = true,
                Password = PasswordHesher.HeshPassword("Admin123$"),
                Role = "Admin"
            } });
        }
    }
}

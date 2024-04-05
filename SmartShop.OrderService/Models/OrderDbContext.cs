using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace SmartShop.OrderService.Models
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options){}

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}

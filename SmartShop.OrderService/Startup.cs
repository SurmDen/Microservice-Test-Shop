using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityManager.Ifrastructure;
using SmartShop.OrderService.Models;
using Microsoft.EntityFrameworkCore;
using SmartShop.OrderService.ProtoServices;

namespace SmartShop.OrderService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddJwtBearerAuthentication();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartShop.OrderService", Version = "v1" });
            });
            services.AddDbContext<OrderDbContext>(options=> 
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SmartShop.OrderService"]);
            });
            services.AddTransient<IOrderRepository, OrderRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartShop.OrderService v1"));
            }
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<OrderServiceProtoModel>();
            });
        }
    }
}

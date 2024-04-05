using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityManager.Ifrastructure;
using SmartShop.AccountService.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using SmartShop.AccountService.ProtoServices;

namespace SmartShop.AccountService
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
            services.AddSingleton<ICurrentUserSaver, CurrentUserSaver>();
            services.AddSwaggerGen();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddJwtBearerAuthentication();
            services.AddControllers();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<AccountDbContext>(options=> 
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SmartShop.AccountService"]);
            });
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSession();
            app.UseRouting();
            app.Use(async (context, next) =>
            {

                
                await next();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<AccountServiceProtoModel>();
                endpoints.MapControllers();
                
            });
            
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}

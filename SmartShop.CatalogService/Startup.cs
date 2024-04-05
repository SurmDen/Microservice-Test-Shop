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
using IdentityManager.Models;
using Microsoft.EntityFrameworkCore;
using SmartShop.CatalogService.Models;
using SmartShop.CatalogService.ProtoServices;

namespace SmartShop.CatalogService
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
            services.AddJwtBearerAuthentication();
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartShop.CatalogService", Version = "v1" });
            });
            
            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SmartShop.CatalogService"]);
            });

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartShop.CatalogService v1"));
            }

            app.UseSession();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<CatalogServiceProtoModel>();
                endpoints.MapControllers();
            });
        }
    }
}

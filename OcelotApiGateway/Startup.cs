using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityManager.Ifrastructure;
using IdentityManager.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotApiGateway.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace OcelotApiGateway
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options=> 
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
            });
            services.AddDistributedMemoryCache();
            services.AddOcelot(Configuration);
            services.AddJwtBearerAuthentication();
            services.AddSingleton<ITokenSaver, TokenSaver>();
            services.AddRazorPages();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITokenSaver saver)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.AddJwtBearerHeader();

            app.Use(async (context, next) =>
            {
                if (string.IsNullOrEmpty(context.Request.Headers["Authorization"]))
                {
                    if (saver.SetToken() == null)
                    {
                        saver.SaveToken(context.Session.GetString("token"));
                    }
                }

                context.Request.Headers.Add("Authorization", "Bearer " + saver.SetToken());
                context.Response.Headers.Add("Authorization", "Bearer " + saver.SetToken());

                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapRazorPages();
            });

            app.UseOcelot().Wait();
        }
    }
}

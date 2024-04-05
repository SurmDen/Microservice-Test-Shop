using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using IdentityManager.Models;

namespace IdentityManager.Ifrastructure
{
    public static class TokenServiceExtentions
    {
        public static void AddTokenService(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
        }
    }
}

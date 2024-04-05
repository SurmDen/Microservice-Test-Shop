using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace OcelotApiGateway.Infrastructure
{
    public static class ApplicationBuilderExtentions
    {
        public static void AddJwtBearerHeader(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<JwtTokenMiddleware>();
        }
    }
}

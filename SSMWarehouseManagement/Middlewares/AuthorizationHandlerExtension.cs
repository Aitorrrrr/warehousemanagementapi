using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSMWarehouseManagement.Middlewares
{
    public static class AuthorizationHandlerExtension
    {
        public static IApplicationBuilder UseAuthorizationHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationHandlerMiddleware>();
        }
    }
}

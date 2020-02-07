using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMWarehouseManagement.Middlewares
{
    public class AuthorizationHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/index.html" || context.Request.Path.StartsWithSegments("/swagger"))
            {
                // Funcionalidad de swagger
                await _next.Invoke(context);
            }
            else
            {
                // Buscamos header en la llamada http
                string authHeader = context.Request.Headers["X-API-KEY"];
                if (authHeader != null && authHeader.StartsWith("ApiKey"))
                {
                    string header = authHeader.Substring("ApiKey ".Length).Trim();
                    // Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                    // autorizacion = encoding.GetString(Convert.FromBase64String(header));

                    if (header == "haha123")
                    {
                        await _next.Invoke(context);
                    }
                    else
                    {
                        // El token no era válido o estaba vacío
                        context.Response.StatusCode = 401;
                        return;
                    }
                }
                else
                {
                    // Sin header de autorización, en este caso se llama 'X-API-KEY'
                    context.Response.StatusCode = 401;
                    return;
                }
            }
        }
    }
}
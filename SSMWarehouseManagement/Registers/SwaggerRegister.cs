using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SSMWarehouseManagement.Registers
{
    public static class SwaggerRegister
    {
        public static IServiceCollection addSwaggerRegisters(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SSM Warehouse Management",
                    Version = "v1.0",
                    Description = "API PDA's entradas",

                    Contact = new OpenApiContact
                    {
                        Name = "Sim Levante",
                        Email = "soporte@simlevante.es",
                        Url = new Uri("http://www.simlevante.com/"),
                    }
                });

                c.AddSecurityDefinition("ApiKeyAuth", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Introduzca token de autorización",
                    Name = "X-API-KEY",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKeyAuth"
                            }
                        },
                        new string [] {}
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}

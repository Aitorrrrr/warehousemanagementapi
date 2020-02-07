using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSMWarehouseManagement.Registers
{
    public static class RepositoriesRegister
    {
        public static IServiceCollection addRepositoriesRegisters(this IServiceCollection services)
        {
            var allProviderTypes = System.Reflection.Assembly.GetExecutingAssembly()
                                                             .GetTypes()
                                                             .Where(t => t.Namespace != null && t.Namespace.Contains("Repositories"));

            foreach (var clases in allProviderTypes.Where(x => x.IsClass).Where(x => x.Name.Contains("Repository")))
            {
                services.AddTransient(clases);
            }

            return services;
        }
    }
}
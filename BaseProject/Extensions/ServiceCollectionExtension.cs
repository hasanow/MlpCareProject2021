using BaseProject.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDependencyResolver(this IServiceCollection services, IBaseModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(services);                
            }
            
            return ServiceTool.Create(services);
        }
    }
}

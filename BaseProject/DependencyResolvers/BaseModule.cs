using BaseProject.CrossCuttingConcerns.Caching;
using BaseProject.CrossCuttingConcerns.Caching.MicrosoftCache;
using BaseProject.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace BaseProject.DependencyResolvers
{
    public class BaseModule : IBaseModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();            
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<Stopwatch>(); 
        }    

    }
}

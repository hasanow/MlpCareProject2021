using Autofac;
using Autofac.Extras.DynamicProxy;
using BaseProject.CrossCuttingConcerns.Caching;
using BaseProject.CrossCuttingConcerns.Caching.Redis;
using BaseProject.Utilities.Interceptors;
using BaseProject.Utilities.Security.Jwt;
using Business.Abstract.EntityServices;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.AutoFac
{
    public class AutoFacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MlpCareDbContext>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<JWTHelper>().As<ITokenHelper>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<RedisCacheManager>().As<ICacheManager>();

            var assebly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assebly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
            base.Load(builder);
        }
    }
}

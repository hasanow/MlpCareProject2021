using BaseProject.DependencyResolvers;
using BaseProject.Utilities.IoC;
using BaseProject.Utilities.Security.Encryption;
using BaseProject.Utilities.Security.Jwt;
using BaseProject.Extensions;
using Business.DependencyResolvers.AutoMapper;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Business.Concrete.EntityFramework;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Business.Abstract.EntityServices;

namespace NetCoreApiWithAngulr
{
    public class Startup
    {
       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowOrigin", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            });
            services.AddDbContext<MlpCareDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration["ConnectionStrings:MlpCareDbConnection"]);                
            });
            
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MlpCareProject", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name="Authorization",
                    Type=SecuritySchemeType.ApiKey,
                    Scheme="Bearer",
                    BearerFormat="JWT",
                    In=ParameterLocation.Header,
                    Description = "JWT Authorization header usign the Bearer scheme"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                         new OpenApiSecurityScheme
                         {
                             Reference=new OpenApiReference
                             {
                                 Type=ReferenceType.SecurityScheme,
                                 Id="Bearer"
                             }
                         },
                         new string[]{ }
                         }
                });
            });

            
            var tokenOption = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                    {
                        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidIssuer = tokenOption.Issuer,
                            ValidAudience = tokenOption.Audience,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOption.SecurityKey)
                        };
                    });
            MlpCareDbContext.SetEncryptionKey(Configuration.GetSection("DbEncryptionKey").Get<string>());
            
            services.AddDependencyResolver(new IBaseModule[] {
                new BaseModule()
            });

            services.AddStackExchangeRedisCache(a =>
            {
                a.Configuration = "localhost:6379";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MlpCareProject v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MlpCareDbContext>();
                context.Database.Migrate();
                if (!context.Users.Any())
                {
                    new AuthManager(new JWTHelper(Configuration),new EfUserDal(new MlpCareDbContext())).Register(new Entities.Dtos.UserForRegisterDto()
                    {
                        FirstName = "Hüseyin",
                        LastName = "Keskintaþ",
                        Email = "keskintashuseyin@gmail.com",
                        Password = "mlpcare",
                        TcKimlikNo = "45432322148"
                    });
                }
            }
            app.ConfigureCustomExceptionMiddleware();
            app.UseCors(builder=>builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}

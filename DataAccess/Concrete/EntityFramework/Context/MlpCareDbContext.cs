using BaseProject.Entities.Concrete;
using BaseProject.Utilities.Security.Encryption;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class MlpCareDbContext:DbContext
    {

        private static IConfiguration Configuration;
        private static string DbEncrptionKey { get; set; }
        public static void SetEncryptionKey(string key)
        {
            DbEncrptionKey = key;
            EncryptionHelper.SetEncryptKey(key);
        }

        public MlpCareDbContext(DbContextOptions<MlpCareDbContext> options,IConfiguration configuration): base(options)
        {
            Configuration = configuration;
            
        }
        public MlpCareDbContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MlpCareDbConnection"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(new EncryptionHelper());
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User_T> Users { get; set; }
    }
}

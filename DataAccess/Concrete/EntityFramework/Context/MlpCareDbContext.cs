using BaseProject.Entities.Concrete;
using BaseProject.Utilities.Security.Encryption;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class MlpCareDbContext:DbContext
    {
        private static string DbEncrptionKey { get; set; }
        public static void SetEncryptionKey(string key)
        {
            DbEncrptionKey = key;
            EncryptionHelper.SetEncryptKey(key);
        }

        public MlpCareDbContext(DbContextOptions<MlpCareDbContext> options): base(options)
        {
        }
        public MlpCareDbContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\\\mssqllocaldb;Database=MlpCareProjectDb;Trusted_Connection=True;MultipleActiveResultSets=true;");
            
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

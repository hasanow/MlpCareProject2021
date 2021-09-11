using Business.Abstract.EntityServices;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class SeedDb
    {
        private IAuthService authService;
        private MlpCareDbContext context;
        public SeedDb(MlpCareDbContext context, IAuthService authService)
        {
            this.authService = authService;
            this.context = context;
        }
        public void Initialize()
        {
            if (context.Database.EnsureCreated())
            {
               
            }
        }
    }
}

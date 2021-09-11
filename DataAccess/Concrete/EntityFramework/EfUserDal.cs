using BaseProject.DataAccess.EntityFramework;
using BaseProject.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal:EfEntityRepositoryBase<User_T,MlpCareDbContext>,IUserDal
    {
        public EfUserDal(MlpCareDbContext context):base(context)
        {

        }
    }
}

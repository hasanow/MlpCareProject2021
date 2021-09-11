using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseProject.Entities.Concrete;
using BaseProject.Utilities.Results;

namespace Business.Abstract.EntityServices
{
    public interface IUserService
    {
        IDataResult<List<User_T>> GetList();
        IDataResult<User_T> Get(int userId);
        IDataResult<User_T> GetByMail(string mail);        
        IDataResult<User_T> Save(User_T user);
        IResult Delete(int userId);
        
    }
}

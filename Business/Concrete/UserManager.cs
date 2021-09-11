using AutoMapper;
using BaseProject.Aspects.AutoFac.Caching;
using BaseProject.Aspects.AutoFac.Validation;
using BaseProject.Entities.Concrete;
using BaseProject.Utilities.Results;
using BaseProject.Utilities.Security.Hashing;
using Business.Abstract.EntityServices;
using Business.Aspects.AutoFac;
using Business.Contants;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    [SecuredOperation()]
    public class UserManager : IUserService
    {

        IUserDal userDal;

        public UserManager(IUserDal userDal)
        {
            this.userDal = userDal;
        }
        [CacheRemoveAspet("IUserService.Get")]
        [CacheRemoveAspet("IUserService.GetList")]
        public IResult Delete(int userId)
        {
            try
            {
                User_T user = new User_T() { UserId = userId };
                userDal.Delete(user);
                return new SuccessResult();
            }
            catch (Exception)
            {
                return new ErrorResult();
            }
        }
        [CacheAspect(5)]
        public IDataResult<User_T> Get(int userId)
        {

          return new SuccessDataResult<User_T>(userDal.Get(u => u.UserId == userId));
        }


        public IDataResult<User_T> GetByMail(string mail)
        {
            return new SuccessDataResult<User_T>(userDal.Get(u => u.Email == mail));
        }

        [CacheAspect(5)]
        public IDataResult<List<User_T>> GetList()
        {
            return new SuccessDataResult<List<User_T>>(userDal.GetList().ToList());
        }

        [ValidationAspect(typeof(UserValidator),Priority =1)]
        [CacheRemoveAspet("IUserService.Get")]
        [CacheRemoveAspet("IUserService.GetList")]
        public IDataResult<User_T> Save(User_T user)
        {

            if (user.UserId != 0)
                return new SuccessDataResult<User_T>(userDal.Update(user));
            else
                return new SuccessDataResult<User_T>(userDal.Add(user));
        }
    }
}

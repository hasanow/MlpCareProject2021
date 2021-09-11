using AutoMapper;
using BaseProject.Aspects.AutoFac.Caching;
using BaseProject.Aspects.AutoFac.Validation;
using BaseProject.Entities.Concrete;
using BaseProject.Utilities.Results;
using BaseProject.Utilities.Security.Hashing;
using BaseProject.Utilities.Security.Jwt;
using Business.Abstract.EntityServices;
using Business.Aspects.AutoFac;
using Business.Concrete.EntityFramework;
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
    public class AuthManager:IAuthService
    {
        private IUserDal userDal;
        private ITokenHelper tokenHelper;

        public AuthManager(ITokenHelper tokenHelper,IUserDal userDal)
        {
            this.tokenHelper = tokenHelper;
            this.userDal = userDal;
        }

        public IDataResult<AccessToken> CreateAccessToken(User_T user)
        {            
            var token = tokenHelper.CreateToken(user,null);
            return new SuccessDataResult<AccessToken>(token, Messages.AccessTokenCreated);
        }       

        public IDataResult<User_T> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = GetByMail(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<User_T>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User_T>(Messages.PasswordError);
            }

            return new SuccessDataResult<User_T>(userToCheck, Messages.SuccessfulLogin);
        }

        [SecuredOperation(Priority =1)]
        [ValidationAspect(typeof(UserForRegisterDtoValidator),Priority =2)]
        [CacheRemoveAspet("IUserService.Get")]
        [CacheRemoveAspet("IUserService.GetList")]
        public IDataResult<User_T> Register(UserForRegisterDto userForRegisterDto)
        {
            IResult result;

            if (userForRegisterDto.UserId == 0)
                result = UserExist(userForRegisterDto.Email);
            else result = new Result(true);

            if (result.Success)
            {
                
                User_T user;
                if (userForRegisterDto.UserId != 0)
                    user = GetUserWithId(userForRegisterDto.UserId);
                else
                    user = new User_T();

                user.Email = userForRegisterDto.Email;
                user.FirstName = userForRegisterDto.FirstName;
                user.LastName = userForRegisterDto.LastName;
                user.TcKimlikNo = userForRegisterDto.TcKimlikNo;
                user.Status = true;
                if (!string.IsNullOrEmpty(userForRegisterDto.Password))
                {
                    byte[] passwordHash, passwordSalt;
                    HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                }

                return Save(user);
            }
            else
            {
                return new ErrorDataResult<User_T>(result.Message);
            }
        }

        public IResult UserExist(string email)
        {
            var result = GetByMail(email);
            if (result.Success && result.Data != null)
                return new ErrorResult(Messages.UserAlreadyExists);

            return new SuccessResult();
        }
        private IDataResult<User_T> GetByMail(string mail)
        {
            return new SuccessDataResult<User_T>(userDal.Get(u => u.Email == mail));
        }
        [CacheRemoveAspet("IUserService.Get")]
        [CacheRemoveAspet("IUserService.GetList")]
        private IDataResult<User_T> Save(User_T user)
        {

            if (user.UserId != 0)
                return new SuccessDataResult<User_T>(userDal.Update(user));
            else
                return new SuccessDataResult<User_T>(userDal.Add(user));
        }
        private User_T GetUserWithId(int userId)
        {

            return userDal.Get(u => u.UserId == userId);
        }
    }
}

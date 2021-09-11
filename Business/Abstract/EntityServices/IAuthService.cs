using BaseProject.Entities.Concrete;
using BaseProject.Utilities.Results;
using BaseProject.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.EntityServices
{
    public interface IAuthService
    {
        IDataResult<User_T> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<User_T> Login(UserForLoginDto userForLoginDto);
        IResult UserExist(string email);
        IDataResult<AccessToken> CreateAccessToken(User_T user);
    }
}

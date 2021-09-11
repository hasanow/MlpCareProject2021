using BaseProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User_T user, List<OperationClaim> operationClaims);
    }
}

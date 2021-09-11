using BaseProject.Entities.Concrete;
using BaseProject.Extensions;
using BaseProject.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Utilities.Security.Jwt
{
    public class JWTHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; set; }
        private TokenOptions tokenOption { get; set; }
        public DateTime accessTokenExpiration { get; set; }
        public JWTHelper(IConfiguration configuration)
        {
            this.tokenOption = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            this.Configuration = configuration;
        }

        public AccessToken CreateToken(User_T user, List<OperationClaim> operationClaims)
        {
            accessTokenExpiration = DateTime.Now.AddMinutes(tokenOption.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(tokenOption.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(tokenOption, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken()
            {
                Token = token,
                Expiration = accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User_T user,
                                                       SigningCredentials signingCredentials, List<OperationClaim> claims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, claims),
                signingCredentials: signingCredentials
                );

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User_T user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.UserId);
            claims.AddEmail(user.Email);
            claims.AddName(user.FirstName + " " + user.LastName);
            claims.AddRoles(operationClaims?.Select(o => o.Name));
            return claims;
        }
    }
}

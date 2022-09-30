using Dragon.Core.Common;
using Dragon.Core.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions
{
    public class JwtHelper
    {
        public static bool CustomSafeVerify(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var symmetricKeyAsBase64 = Appsettings.app(new string[] { "JwtSettings", "SecretKey" });
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = jwtHandler.ReadJwtToken(token);
            return jwt.RawSignature == Microsoft.IdentityModel.JsonWebTokens.JwtTokenUtilities.CreateEncodedSignature(jwt.RawHeader + "." + jwt.RawPayload, signingCredentials);
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModelJwt SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            TokenModelJwt tokenModelJwt = new TokenModelJwt();

            // token校验
            if (jwtStr.IsNotEmptyOrNull() && jwtHandler.CanReadToken(jwtStr))
            {

                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);

                object role;

                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);

                tokenModelJwt = new TokenModelJwt
                {
                    Uid = jwtToken.Id,
                    Role = role != null ? role.ObjToString() : "",
                };
            }
            return tokenModelJwt;
        }

    }

    
}

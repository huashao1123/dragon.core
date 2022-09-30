using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Common
{
    public interface IUser
    {
        string Name { get; }
        string ID { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
        List<string> GetClaimValueByType(string ClaimType);

        string GetToken();
        List<string> GetUserInfoFromToken(string ClaimType);
    }
}

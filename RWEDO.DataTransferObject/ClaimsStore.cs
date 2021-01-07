using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RWEDO.DataTransferObject
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Read", "Read"),
            new Claim("Write","Write"),
            new Claim("Delete","Delete")
        };
    }
}

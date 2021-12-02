using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Framework.Extensions
{
    public static class Claims
    {
        public static T ParseClaim<T>(this List<Claim> claims, string type)
        {
            var claim = claims.Find(c => c.Type == type);
            return claim != null ? (T)Convert.ChangeType(claim.Value, typeof(T)) : default;
        }
    }
}

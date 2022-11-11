using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OuroVerde.Maintenance.Domain.Core.Authorization
{
    public static class CustomAuthorizationValidation
    {
        public static bool UserHasValidClaim(HttpContext context, string claimName, string claimValue)
        {
            if (context.User.Identity!.IsAuthenticated)
            {
                return context.User.Claims.Any((Claim c) => c.Type == claimName && (from v in c.Value.Split(',')
                                                                                    select v.Trim()).Contains(claimValue));
            }

            return false;
        }
    }
}

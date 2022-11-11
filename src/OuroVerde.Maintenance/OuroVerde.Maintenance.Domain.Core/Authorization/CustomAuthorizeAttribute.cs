using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OuroVerde.Maintenance.Domain.Core.Authorization
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(string claimName, string claimValue)
            : base(typeof(RequerimentClaimFilter))
        {
            base.Arguments = new object[1]
            {
                new Claim(claimName, claimValue)
            };
        }
    }
}

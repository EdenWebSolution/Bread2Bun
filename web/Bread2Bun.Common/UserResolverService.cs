using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Bread2Bun.Common
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;
        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public ClaimsPrincipal User
        {
            get
            {
                return _context.HttpContext?.User;
            }
        }

        public long GetUser()
        {
            var identity = _context.HttpContext.User.Identity as ClaimsIdentity;
            return 2;//_context.HttpContext.User?.Identity?.Name;
        }

       
    }
}

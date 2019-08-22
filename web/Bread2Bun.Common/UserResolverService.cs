using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Bread2Bun.Common
{
    public class UserResolverService
    {
        private IEnumerable<Claim> Claims { get; set; }
        private readonly IHttpContextAccessor _context;
        public UserResolverService(IHttpContextAccessor context)
        {
            Claims = new List<Claim>();
            _context = context;
            Claims = SetClaims();
        }


        private IEnumerable<Claim> SetClaims()
        {
            return _context.HttpContext.User.Claims;
        }

        public long UserId
        {
            get
            {
                return 1;
            }
        }
    }
}

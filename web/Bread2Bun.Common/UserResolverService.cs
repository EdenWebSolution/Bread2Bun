using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Bread2Bun.Common
{
    [NotMapped]
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;

        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public long UserId
        {
            get
            {
                var hasUserId = long.TryParse(_context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId);

                if (hasUserId)
                {
                    return userId;
                }
                else
                {
                    throw new AuthenticationException("Your session has expired, login again");
                }
            }
        }
    }
}

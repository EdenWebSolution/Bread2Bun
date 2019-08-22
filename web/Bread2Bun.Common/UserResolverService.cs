using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
                return long.Parse(_context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
        }
    }
}

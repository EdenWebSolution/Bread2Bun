using AutoMapper;
using Bread2Bun.Data;
using Bread2Bun.Service.Security.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly Bread2BunContext bread2BunContext;
        private readonly IMapper mapper;

        public SecurityService(Bread2BunContext bread2BunContext, IMapper mapper)
        {
            this.bread2BunContext = bread2BunContext;
            this.mapper = mapper;
        }
    }
}

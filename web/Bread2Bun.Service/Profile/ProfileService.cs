using AutoMapper;
using Bread2Bun.Data;
using Bread2Bun.Service.Profile.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly Bread2BunContext bread2BunContext;
        private readonly IMapper mapper;

        public ProfileService(Bread2BunContext bread2BunContext, IMapper mapper)
        {
            this.bread2BunContext = bread2BunContext;
            this.mapper = mapper;
        }

    }
}

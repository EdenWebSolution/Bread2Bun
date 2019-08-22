using Bread2Bun.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service
{
    public class ServiceBase
    {
        public ServiceBase(UserResolverService userResolverService)
        {
          var asda=  userResolverService.GetUser();
        }
    }
}

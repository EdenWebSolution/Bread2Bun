using Bread2Bun.Service.Security;
using Bread2Bun.Service.Security.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bread2Bun.Web
{
    public static class ServiceInjector
    {
        public static void InjectServices(IServiceCollection services)
        {
            services.AddScoped<ISecurityService, SecurityService>();
        }
    }
}

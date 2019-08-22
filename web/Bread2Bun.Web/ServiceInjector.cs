using Bread2Bun.Common;
using Bread2Bun.Data;
using Bread2Bun.Service.Country;
using Bread2Bun.Service.Country.Interface;
using Bread2Bun.Service.Profile;
using Bread2Bun.Service.Profile.Interface;
using Bread2Bun.Service.Security;
using Bread2Bun.Service.Security.Interface;
using Bread2Bun.Service.University;
using Bread2Bun.Service.University.Interface;
using Microsoft.AspNetCore.Http;
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
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IUniversityService, UniversityService>();
            services.AddScoped<UserResolverService>();
            services.AddScoped<IProfileService, ProfileService>();
        }
    }
}

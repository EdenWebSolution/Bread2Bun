using Bread2Bun.Common;
using Bread2Bun.Service.Chat.Interface;
using Bread2Bun.Service.Chat.Service;
using Bread2Bun.Service.Country;
using Bread2Bun.Service.Country.Interface;
using Bread2Bun.Service.Food;
using Bread2Bun.Service.Food.Interface;
using Bread2Bun.Service.Profile;
using Bread2Bun.Service.Profile.Interface;
using Bread2Bun.Service.Security;
using Bread2Bun.Service.Security.Interface;
using Bread2Bun.Service.University;
using Bread2Bun.Service.University.Interface;
using Bread2Bun.Web.AppHubs;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IFoodService, FoodService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddSingleton<AllConnectedUsers>();
        }
    }
}

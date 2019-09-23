using Bread2Bun.Common.Model;
using Bread2Bun.Domain.Food;
using Bread2Bun.Domain.Security;
using Bread2Bun.Domain.UserProfile;
using Bread2Bun.Service.Food.Models;
using Bread2Bun.Service.Profile.Models;
using Bread2Bun.Service.Profile.Models.Review;
using Bread2Bun.Service.Profile.Models.UserProfile;
using Bread2Bun.Service.Security.Models;

namespace Bread2Bun.Service
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateStoreUserModel, StoreUser>().ReverseMap();
            CreateMap<StoreUser, StoreUserModel>().ReverseMap();
            CreateMap<CountryModel, Bread2Bun.Domain.Shared.Country>().ReverseMap();
            CreateMap<UniversityModel, Bread2Bun.Domain.Shared.University>().ReverseMap();
            CreateMap<StoreUser, BasicInfoModel>();
            CreateMap<ReviewCreateModel, Reviews>();
            CreateMap<Reviews, ReviewModel>();
            CreateMap<ReviewUpdateModel, Reviews>();
            CreateMap<UserProfileCreateModel, UserProfile>();
            CreateMap<UserProfile, UserProfileModel>();
            CreateMap<UserProfile, UserProfileUpdateModel>();
            CreateMap<Domain.Food.Food, FoodModel>().ReverseMap();
        }
    }
}

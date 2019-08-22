using Bread2Bun.Common.Model;
using Bread2Bun.Domain.Security;
using Bread2Bun.Service.Profile.Models;
using Bread2Bun.Service.Security.Models;

namespace Bread2Bun.Service
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateStoreUserModel, StoreUser>().ReverseMap();
            CreateMap<StoreUser, StoreUserModel>();
            CreateMap<CountryModel, Bread2Bun.Domain.Shared.Country>().ReverseMap();
            CreateMap<UniversityModel, Bread2Bun.Domain.Shared.University>().ReverseMap();
            CreateMap<StoreUser, BasicInfoModel>();
        }
    }
}

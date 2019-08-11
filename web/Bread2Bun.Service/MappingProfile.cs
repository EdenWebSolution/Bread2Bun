using AutoMapper;
using Bread2Bun.Common.Model;
using Bread2Bun.Domain.Security;
using Bread2Bun.Service.Country.Model;
using Bread2Bun.Service.Security.Models;
using Bread2Bun.Service.University.Model;

namespace Bread2Bun.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateStoreUserModel, StoreUser>().ReverseMap();
            CreateMap<StoreUser, StoreUserModel>();
            CreateMap<CountryModel, Bread2Bun.Domain.Shared.Country>().ReverseMap();
            CreateMap<UniversityModel, Bread2Bun.Domain.Shared.University>().ReverseMap();
        }
    }
}

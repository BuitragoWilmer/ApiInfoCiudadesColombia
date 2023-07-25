 using AutoMapper;
using InfoCity.API.Entities;
using InfoCity.API.Model;

namespace InfoCity.API.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityWithoutPointInterestDto>()
                .ForMember(x=>x.Id, y=>y.MapFrom(s=>s.CityId));
            CreateMap<City, CityDto>()
                .ForMember(x => x.Id, y => y.MapFrom(s => s.CityId));
        }

    }
}

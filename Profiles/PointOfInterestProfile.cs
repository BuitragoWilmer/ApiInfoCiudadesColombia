using AutoMapper;
using InfoCity.API.Entities;
using InfoCity.API.Model;

namespace InfoCity.API.Profiles
{
    public class PointOfInterestProfile: Profile
    {
        public PointOfInterestProfile() 
        {
            CreateMap<PointOfInterest, PointInterestDto>()
                    .ForMember(x => x.Id, y => y.MapFrom(s => s.PointInterestId));

            CreateMap<PointInterestCreationDto, PointOfInterest>();

            CreateMap<PointInterestUpdateDto, PointOfInterest>();

            CreateMap<PointOfInterest, PointInterestUpdateDto>();
        }
    }
}

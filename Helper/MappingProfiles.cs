using AutoMapper;
using bankingApplication_API.Dto;
using bankingApplication_API.Models;

namespace bankingApplication_API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<NaturalPersonDto, NaturalPerson>()
                .ForMember(dest => dest.id, opt => opt.Ignore());
        }
    }
}

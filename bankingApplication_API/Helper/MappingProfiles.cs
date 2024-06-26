﻿using AutoMapper;

using bankingApplication_API.Dto;
using bankingApplication_API.Models;
using bankingApplication_API.Validators;

namespace bankingApplication_API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<NaturalPersonDto, NaturalPersonValidator>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<NaturalPersonValidator, NaturalPerson>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<JuridicalPersonDto, JuridicalPersonValidator>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<JuridicalPersonValidator, JuridicalPerson>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }        
    }
}

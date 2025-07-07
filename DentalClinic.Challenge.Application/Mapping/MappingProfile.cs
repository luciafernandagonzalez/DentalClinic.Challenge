using AutoMapper;
using DentalClinic.Challenge.Application.DTOs;
using DentalClinic.Challenge.Core.Entities;

namespace DentalClinic.Challenge.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Specialty, SpecialtyDto>()
                .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => Convert.ToBase64String(src.RowVersion ?? new byte[0])));

            CreateMap<SpecialtyDto, Specialty>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.RowVersion) ? null : Convert.FromBase64String(src.RowVersion)
                ));
        }
    }
}

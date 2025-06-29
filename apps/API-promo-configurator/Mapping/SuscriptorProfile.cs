using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;
using AutoMapper;

namespace API_promo_configurator.Mapping;

public class SuscriptorProfile : Profile
{
    public SuscriptorProfile()
    {
        CreateMap<Suscriptore, SuscriptorDto>()
            .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => src.FechaRegistro.ToDateTime(TimeOnly.MinValue)))
            .ReverseMap()
            .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaRegistro)));

    }
} 
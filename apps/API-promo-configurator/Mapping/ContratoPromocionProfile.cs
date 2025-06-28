using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;
using AutoMapper;

namespace API_promo_configurator.Mapping;

public class ContratoPromocionProfile : Profile
{
    public ContratoPromocionProfile()
    {
        CreateMap<ContratoPromocione, ContratoPromocionDto>()
            .ForMember(dest => dest.FechaAplicacion, opt => opt.MapFrom(src => src.FechaAplicacion.ToDateTime(TimeOnly.MinValue)))
            .ReverseMap()
            .ForMember(dest => dest.FechaAplicacion, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaAplicacion)));
    }
} 
using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;
using AutoMapper;

namespace API_promo_configurator.Mapping;

public class PromocionCompletaProfile : Profile
{
    public PromocionCompletaProfile()
    {
        CreateMap<PromocionCompletaDto, Promocione>()
            .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaInicio)))
            .ForMember(dest => dest.FechaFin, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaFin)));
    }
} 
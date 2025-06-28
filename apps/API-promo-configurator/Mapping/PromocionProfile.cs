using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;
using AutoMapper;

namespace API_promo_configurator.Mapping;

public class PromocionProfile : Profile
{
    public PromocionProfile()
    {
        CreateMap<Servicio, ServicioDto>().ReverseMap();

        CreateMap<Promocione, PromocionDto>()
            .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => src.FechaInicio.ToDateTime(TimeOnly.MinValue)))
            .ForMember(dest => dest.FechaFin, opt => opt.MapFrom(src => src.FechaFin.ToDateTime(TimeOnly.MinValue)));

        CreateMap<PromocionDto, Promocione>()
            .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaInicio)))
            .ForMember(dest => dest.FechaFin, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaFin)));
    }

}
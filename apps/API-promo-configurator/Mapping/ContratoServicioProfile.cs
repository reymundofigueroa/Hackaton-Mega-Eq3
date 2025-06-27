using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;
using AutoMapper;

namespace API_promo_configurator.Mapping;

public class ContratoServicioProfile : Profile
{
    public ContratoServicioProfile()
    {
        CreateMap<ContratoServicio, ContratoServicioDto>()
            .ForMember(dest => dest.FechaAlta, opt => opt.MapFrom(src => src.FechaAlta.ToDateTime(TimeOnly.MinValue)))
            .ReverseMap()
            .ForMember(dest => dest.FechaAlta, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaAlta)));
    }
}
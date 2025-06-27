using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;
using AutoMapper;

namespace API_promo_configurator.Mapping;

public class ContratoProfile : Profile
{
    public ContratoProfile()
    {
        CreateMap<Contrato, ContratoDto>()
            .ForMember(dest => dest.FechaContratacion, opt => opt.MapFrom(src => src.FechaContratacion.ToDateTime(TimeOnly.MinValue)))
            .ReverseMap()
            .ForMember(dest => dest.FechaContratacion, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaContratacion)));
    }
}
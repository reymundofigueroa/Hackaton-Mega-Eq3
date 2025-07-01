using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;
using AutoMapper;

namespace API_promo_configurator.Mapping;

public class ContratoProfile : Profile
{
    public ContratoProfile()
    {
        CreateMap<Suscriptore, SuscriptorContratoDto>().ReverseMap();

        CreateMap<Contrato, ContratoDto>()
            .ForMember(dest => dest.FechaContratacion, opt => opt.MapFrom(src => src.FechaContratacion.ToDateTime(TimeOnly.MinValue)))
            .ForMember(dest => dest.suscriptor, opt => opt.MapFrom(src => src.Suscriptore))
            .ReverseMap()
            .ForMember(dest => dest.FechaContratacion, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaContratacion)));

    }
}
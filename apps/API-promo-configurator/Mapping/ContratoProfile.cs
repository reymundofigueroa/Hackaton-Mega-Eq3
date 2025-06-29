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
            .ForMember(dest => dest.Servicios, opt => opt.MapFrom(src => src.ContratoServicios))
            .ReverseMap()
            .ForMember(dest => dest.FechaContratacion, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaContratacion)));

        CreateMap<ContratoServicio, ContratoServicioDto>()
            .ForMember(dest => dest.IdServicio, opt => opt.MapFrom(src => src.IdServicio))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Servicio.Nombre))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Servicio.Descripcion))
            .ForMember(dest => dest.PrecioContratado, opt => opt.MapFrom(src => src.PrecioContratado))
            .ForMember(dest => dest.FechaAlta, opt => opt.MapFrom(src => src.FechaAlta));

    }
}
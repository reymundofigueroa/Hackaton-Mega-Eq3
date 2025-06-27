using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;
using AutoMapper;

namespace API_promo_configurator.Mapping;

public class PromocionAlcanceProfile : Profile
{
    public PromocionAlcanceProfile()
    {
        CreateMap<PromocionAlcance, PromocionAlcanceDto>().ReverseMap();
    }
} 
using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;
using AutoMapper;

namespace API_promo_configurator.Mapping;

public class ColoniaProfile : Profile
{
    public ColoniaProfile()
    {
        CreateMap<Colonia, ColoniaDto>().ReverseMap();
    }
} 
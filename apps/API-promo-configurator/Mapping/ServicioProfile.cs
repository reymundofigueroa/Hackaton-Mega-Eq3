using System;
using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;
using AutoMapper;

namespace API_promo_configurator.Mapping;

public class ServicioProfile : Profile
{
    public ServicioProfile()
    {
        // Aqui se convierte el DTO en el Modelo original, si hay mas de 1 DTO se agrega también
        CreateMap<Servicio, ServicioDto>().ReverseMap();
    }
}

using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        // Inyección de dependencias necesario para poder trabajar con los métodos y los DTOs del modelo original
        private readonly IServicioRepository _servicioRepository;
        private readonly IMapper _mapper;

        public ServiciosController(IServicioRepository servicioRepository, IMapper mapper)
        {
            _servicioRepository = servicioRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] // El usuario no está autorizado a acceder a este recurso
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetServicios()
        {
            var servicios = _servicioRepository.GetServicios();
            var serviciosDto = new List<ServicioDto>();

            foreach (var servicio in servicios)
            {
                serviciosDto.Add(_mapper.Map<ServicioDto>(servicio));
            }

            return Ok(serviciosDto);
        }
    }
}

using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IMapper _mapper;

        public SucursalesController(ISucursalRepository sucursalRepository, IMapper mapper)
        {
            _sucursalRepository = sucursalRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSucursales()
        {
            var sucursales = _sucursalRepository.GetSucursales();
            var sucursalesDto = sucursales.Select(s => _mapper.Map<SucursalDto>(s)).ToList();
            return Ok(sucursalesDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSucursal(int id)
        {
            var sucursal = _sucursalRepository.GetSucursal(id);
            if (sucursal == null)
                return NotFound();
            return Ok(_mapper.Map<SucursalDto>(sucursal));
        }
    }
} 
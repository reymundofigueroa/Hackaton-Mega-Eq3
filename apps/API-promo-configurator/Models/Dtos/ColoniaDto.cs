using System;

namespace API_promo_configurator.Models.Dtos;

public class ColoniaDto
{
    public int IdColonia { get; set; }
    public string Nombre { get; set; } = null!;
    public string CodigoPostal { get; set; } = null!;
    public int IdCiudad { get; set; }
} 
using ProyectoCursoAPI.Domain.Entities;
using System;

namespace ProyectoCursoAPI.Application.Features.Proveedores.DTOs
{
    // DTO para exponer informacion de Proveedor
    public class ProveedorResponseDto
    {
        public Guid Id { get; set; }
        public string NombreComercial { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string RFC { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    // Metodos de extension para mapeo entidad -> DTO
    public static class ProveedorMappingExtensions
    {
        public static ProveedorResponseDto ToResponseDto(this Proveedor proveedor)
        {
            return new ProveedorResponseDto
            {
                Id = proveedor.Id,
                NombreComercial = proveedor.NombreComercial,
                Direccion = proveedor.Direccion,
                Telefono = proveedor.Telefono,
                RFC = proveedor.RFC,
                Email = proveedor.Email,
                Activo = proveedor.Activo,
                FechaCreacion = proveedor.FechaCreacion
            };
        }
    }
}

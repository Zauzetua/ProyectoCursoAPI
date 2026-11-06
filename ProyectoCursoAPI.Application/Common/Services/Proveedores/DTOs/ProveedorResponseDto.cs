using ProyectoCursoAPI.Domain.Entities;
using System;

namespace ProyectoCursoAPI.Application.Common.Services.Proveedores.DTOs
{
    public record ProveedorResponseDto
    {
        public Guid Id { get; init; }
        public string NombreComercial { get; init; } = string.Empty;
        public string Direccion { get; init; } = string.Empty;
        public string Telefono { get; init; } = string.Empty;
        public string RFC { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public bool Activo { get; init; }
        public DateTime FechaCreacion { get; init; }
    }

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

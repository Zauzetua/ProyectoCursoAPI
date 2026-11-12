using System;

namespace ProyectoCursoAPI.Application.Features.Proveedores.DTOs
{
    // DTO para la actualizacion de un proveedor existente
    public class UpdateProveedorDto
    {
        public Guid Id { get; set; }
        public string NombreComercial { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string RFC { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public string Email { get; set; } = string.Empty;
    }
}

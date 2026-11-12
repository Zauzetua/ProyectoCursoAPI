namespace ProyectoCursoAPI.Application.Features.Proveedores.DTOs
{
    // DTO para la creacion de un nuevo proveedor
    public class CreateProveedorDto
    {
        public string NombreComercial { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string RFC { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}

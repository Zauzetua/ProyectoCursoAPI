using System;

namespace ProyectoCursoAPI.Application.Features.Productos.DTOs
{
    // DTO para la actualizacion de un producto existente
    public class UpdateProductoDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public Guid IdCategoria { get; set; }
        public Guid IdProveedor { get; set; }
        public bool Activo { get; set; } = true;
    }
}

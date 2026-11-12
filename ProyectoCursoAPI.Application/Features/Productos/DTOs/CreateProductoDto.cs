using System;

namespace ProyectoCursoAPI.Application.Features.Productos.DTOs
{
    // DTO para la creacion de un nuevo producto
    public class CreateProductoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public Guid IdCategoria { get; set; }
        public Guid IdProveedor { get; set; }
    }
}

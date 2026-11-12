using ProyectoCursoAPI.Domain.Entities;
using System;

namespace ProyectoCursoAPI.Application.Features.Productos.DTOs
{
    // DTO para exponer informacion del Producto en listados y detalles
    public class ProductoResponseDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public Guid IdCategoria { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;
        public Guid IdProveedor { get; set; }
        public string ProveedorNombreComercial { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    // Metodos de extension para mapeo entidad -> DTO
    public static class ProductoMappingExtensions
    {
        public static ProductoResponseDto ToResponseDto(this Producto producto)
        {
            return new ProductoResponseDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                IdCategoria = producto.IdCategoria,
                IdProveedor = producto.IdProveedor,
                Activo = producto.Activo,
                FechaCreacion = producto.FechaCreacion
            };
        }
    }
}

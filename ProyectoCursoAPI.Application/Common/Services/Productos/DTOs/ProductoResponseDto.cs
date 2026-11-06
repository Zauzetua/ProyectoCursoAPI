using ProyectoCursoAPI.Domain.Entities;
using System;

namespace ProyectoCursoAPI.Application.Common.Services.Productos.DTOs
{
    public record ProductoResponseDto
    {
        public Guid Id { get; init; }
        public string Nombre { get; init; } = string.Empty;
        public string Descripcion { get; init; } = string.Empty;
        public decimal Precio { get; init; }
        public int Stock { get; init; }
        public Guid IdCategoria { get; init; }
        public Guid IdProveedor { get; init; }
        public bool Activo { get; init; }
        public DateTime FechaCreacion { get; init; }
    }

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

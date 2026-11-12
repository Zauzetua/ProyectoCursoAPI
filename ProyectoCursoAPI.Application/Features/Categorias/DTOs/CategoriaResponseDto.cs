using ProyectoCursoAPI.Domain.Entities;
using System;

namespace ProyectoCursoAPI.Application.Features.Categorias.DTOs
{
    // DTO para exponer informacion de una Categoria
    public class CategoriaResponseDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    // Metodos de extension para mapeo entidad -> DTO
    public static class CategoriaMappingExtensions
    {
        public static CategoriaResponseDto ToResponseDto(this Categoria categoria)
        {
            return new CategoriaResponseDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                Activo = categoria.Activo,
                FechaCreacion = categoria.FechaCreacion
            };
        }
    }
}

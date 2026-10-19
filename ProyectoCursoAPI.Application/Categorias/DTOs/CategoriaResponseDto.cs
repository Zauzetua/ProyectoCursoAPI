using ProyectoCursoAPI.Domain.Entities;
using System;

namespace ProyectoCursoAPI.Application.Categorias.DTOs
{
    public record CategoriaResponseDto
    {
        public Guid Id { get; init; }
        public string Nombre { get; init; } = string.Empty;
        public string? Descripcion { get; init; }
        public bool Activo { get; init; }
        public DateTime FechaCreacion { get; init; }
    }

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

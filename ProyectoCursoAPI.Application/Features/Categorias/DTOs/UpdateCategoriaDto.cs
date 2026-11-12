using System;

namespace ProyectoCursoAPI.Application.Features.Categorias.DTOs
{
    // DTO para la actualizacion de una categoria existente
    public class UpdateCategoriaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}

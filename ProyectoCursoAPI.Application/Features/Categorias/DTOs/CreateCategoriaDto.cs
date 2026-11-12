namespace ProyectoCursoAPI.Application.Features.Categorias.DTOs
{
    // DTO para la creacion de una categoria
    public class CreateCategoriaDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}

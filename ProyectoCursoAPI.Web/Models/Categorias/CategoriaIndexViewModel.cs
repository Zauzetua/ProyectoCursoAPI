using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Categorias.DTOs;
using ProyectoCursoAPI.Web.Models.Shared;

namespace ProyectoCursoAPI.Web.Models.Categorias
{
    // ViewModel para la vista principal Index de Categorias
    public class CategoriaIndexViewModel
    {
        public PagedResult<CategoriaResponseDto> PagedResult { get; set; } = default!;
        public PaginationInfo Pagination { get; set; } = default!;
        public string? SearchTerm { get; set; }
    }
}

using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Productos.DTOs;
using ProyectoCursoAPI.Web.Models.Shared;

namespace ProyectoCursoAPI.Web.Models.Productos
{
    // ViewModel para la vista principal Index de Productos
    public class ProductoIndexViewModel
    {
        public PagedResult<ProductoResponseDto> PagedResult { get; set; } = default!;
        public PaginationInfo Pagination { get; set; } = default!;
        public string? SearchTerm { get; set; }
    }
}

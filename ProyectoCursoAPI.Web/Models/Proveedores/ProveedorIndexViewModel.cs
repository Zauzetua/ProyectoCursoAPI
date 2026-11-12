using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Proveedores.DTOs;
using ProyectoCursoAPI.Web.Models.Shared;

namespace ProyectoCursoAPI.Web.Models.Proveedores
{
    // ViewModel para la vista principal Index de Proveedores
    public class ProveedorIndexViewModel
    {
        public PagedResult<ProveedorResponseDto> PagedResult { get; set; } = default!;
        public PaginationInfo Pagination { get; set; } = default!;
        public string? SearchTerm { get; set; }
    }
}

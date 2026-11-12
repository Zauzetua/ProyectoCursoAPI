using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Categorias.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Services
{
    // Servicio integrador / fachada para las operaciones sobre Categorias.
    public interface ICategoriaService
    {
        Task<CategoriaResponseDto> CreateAsync(CreateCategoriaDto dto, CancellationToken cancellationToken = default);
        Task<CategoriaResponseDto> UpdateAsync(UpdateCategoriaDto dto, CancellationToken cancellationToken = default);
        Task<CategoriaResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoriaResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PagedResult<CategoriaResponseDto>> GetPagedAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Productos.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Services
{
    // Servicio integrador / fachada para productos
    public interface IProductoService
    {
        Task<ProductoResponseDto> CreateAsync(CreateProductoDto dto, CancellationToken cancellationToken = default);
        Task<ProductoResponseDto> UpdateAsync(UpdateProductoDto dto, CancellationToken cancellationToken = default);
        Task<ProductoResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductoResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PagedResult<ProductoResponseDto>> GetPagedAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

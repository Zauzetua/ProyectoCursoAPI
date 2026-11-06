using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Productos.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Services
{
    public interface IProductoService
    {
        Task<ServiceResponse<ProductoResponseDto>> CreateAsync(CreateProductoDto dto, CancellationToken cancellationToken = default);
        Task<ServiceResponse<ProductoResponseDto>> UpdateAsync(UpdateProductoDto dto, CancellationToken cancellationToken = default);
        Task<ServiceResponse<ProductoResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<IEnumerable<ProductoResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ServiceResponse<PagedResult<ProductoResponseDto>>> GetPagedAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default);
        Task<ServiceResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

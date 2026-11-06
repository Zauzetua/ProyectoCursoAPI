using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Proveedores.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Services
{
    public interface IProveedorService
    {
        Task<ServiceResponse<ProveedorResponseDto>> CreateAsync(CreateProveedorDto dto, CancellationToken cancellationToken = default);
        Task<ServiceResponse<ProveedorResponseDto>> UpdateAsync(UpdateProveedorDto dto, CancellationToken cancellationToken = default);
        Task<ServiceResponse<ProveedorResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<ProveedorResponseDto>> GetByRfcAsync(string rfc, CancellationToken cancellationToken = default);
        Task<ServiceResponse<IEnumerable<ProveedorResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ServiceResponse<PagedResult<ProveedorResponseDto>>> GetPagedAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default);
        Task<ServiceResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

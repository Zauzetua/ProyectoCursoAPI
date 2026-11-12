using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Proveedores.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Services
{
    // Servicio integrador / fachada para proveedores
    public interface IProveedorService
    {
        Task<ProveedorResponseDto> CreateAsync(CreateProveedorDto dto, CancellationToken cancellationToken = default);
        Task<ProveedorResponseDto> UpdateAsync(UpdateProveedorDto dto, CancellationToken cancellationToken = default);
        Task<ProveedorResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ProveedorResponseDto> GetByRfcAsync(string rfc, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProveedorResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PagedResult<ProveedorResponseDto>> GetPagedAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

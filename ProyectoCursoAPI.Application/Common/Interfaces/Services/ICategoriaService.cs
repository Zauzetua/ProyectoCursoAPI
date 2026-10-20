using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Categorias.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Services
{
    public interface ICategoriaService
    {
        Task<ServiceResponse<CategoriaResponseDto>> CreateAsync(CreateCategoriaDto dto, CancellationToken cancellationToken = default);
        Task<ServiceResponse<CategoriaResponseDto>> UpdateAsync(UpdateCategoriaDto dto, CancellationToken cancellationToken = default);
        Task<ServiceResponse<CategoriaResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<IEnumerable<CategoriaResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ServiceResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

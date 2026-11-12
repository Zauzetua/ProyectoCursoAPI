using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Proveedores.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Proveedores.UseCases
{
    public interface IGetPagedProveedoresUseCase
    {
        Task<PagedResult<ProveedorResponseDto>> ExecuteAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Listar proveedores paginados
    public class GetPagedProveedoresUseCase : IGetPagedProveedoresUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPagedProveedoresUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<ProveedorResponseDto>> ExecuteAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default)
        {
            var (items, totalCount) = await _unitOfWork.Proveedores.GetPagedAsync(
                paginationParams.PageNumber,
                paginationParams.PageSize,
                p => p.Activo,
                true,
                cancellationToken
            );

            var dtos = items.Select(p => p.ToResponseDto()).ToList();

            return new PagedResult<ProveedorResponseDto>(
                dtos,
                totalCount,
                paginationParams.PageNumber,
                paginationParams.PageSize
            );
        }
    }
}

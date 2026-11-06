using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Proveedores.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Services.Proveedores.UseCases
{
    public interface IGetPagedProveedoresUseCase
    {
        Task<ServiceResponse<PagedResult<ProveedorResponseDto>>> ExecuteAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default);
    }

    public class GetPagedProveedoresUseCase : IGetPagedProveedoresUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPagedProveedoresUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<PagedResult<ProveedorResponseDto>>> ExecuteAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default)
        {
            var (items, totalCount) = await _unitOfWork.Proveedores.GetPagedAsync(
                paginationParams.PageNumber,
                paginationParams.PageSize,
                asNoTracking: true);

            var dtos = items.Select(p => p.ToResponseDto()).ToList();

            var pagedResult = new PagedResult<ProveedorResponseDto>(
                dtos,
                totalCount,
                paginationParams.PageNumber,
                paginationParams.PageSize);

            return ServiceResponse<PagedResult<ProveedorResponseDto>>.Ok(pagedResult);
        }
    }
}

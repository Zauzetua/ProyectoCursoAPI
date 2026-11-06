using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Productos.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Services.Productos.UseCases
{
    public interface IGetPagedProductosUseCase
    {
        Task<ServiceResponse<PagedResult<ProductoResponseDto>>> ExecuteAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default);
    }

    public class GetPagedProductosUseCase : IGetPagedProductosUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPagedProductosUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<PagedResult<ProductoResponseDto>>> ExecuteAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default)
        {
            var (items, totalCount) = await _unitOfWork.Productos.GetPagedAsync(
                paginationParams.PageNumber,
                paginationParams.PageSize,
                asNoTracking: true);

            var dtos = items.Select(p => p.ToResponseDto()).ToList();

            var pagedResult = new PagedResult<ProductoResponseDto>(
                dtos,
                totalCount,
                paginationParams.PageNumber,
                paginationParams.PageSize);

            return ServiceResponse<PagedResult<ProductoResponseDto>>.Ok(pagedResult);
        }
    }
}

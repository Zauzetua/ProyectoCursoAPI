using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Categorias.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Services.Categorias.UseCases
{
    public interface IGetPagedCategoriasUseCase
    {
        Task<ServiceResponse<PagedResult<CategoriaResponseDto>>> ExecuteAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default);
    }

    public class GetPagedCategoriasUseCase : IGetPagedCategoriasUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPagedCategoriasUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<PagedResult<CategoriaResponseDto>>> ExecuteAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default)
        {
            var (items, totalCount) = await _unitOfWork.Categorias.GetPagedAsync(
                paginationParams.PageNumber,
                paginationParams.PageSize,
                asNoTracking: true);

            var dtos = items.Select(c => c.ToResponseDto()).ToList();

            var pagedResult = new PagedResult<CategoriaResponseDto>(
                dtos,
                totalCount,
                paginationParams.PageNumber,
                paginationParams.PageSize);

            return ServiceResponse<PagedResult<CategoriaResponseDto>>.Ok(pagedResult);
        }
    }
}

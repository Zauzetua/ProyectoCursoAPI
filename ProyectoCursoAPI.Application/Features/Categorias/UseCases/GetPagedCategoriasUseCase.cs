using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Categorias.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Categorias.UseCases
{
    public interface IGetPagedCategoriasUseCase
    {
        Task<PagedResult<CategoriaResponseDto>> ExecuteAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Listar categorias paginadas
    public class GetPagedCategoriasUseCase : IGetPagedCategoriasUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPagedCategoriasUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<CategoriaResponseDto>> ExecuteAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default)
        {
            var (items, totalCount) = await _unitOfWork.Categorias.GetPagedAsync(
                paginationParams.PageNumber,
                paginationParams.PageSize,
                c => c.Activo,
                true,
                cancellationToken
            );

            var dtos = items.Select(c => c.ToResponseDto()).ToList();

            return new PagedResult<CategoriaResponseDto>(
                dtos,
                totalCount,
                paginationParams.PageNumber,
                paginationParams.PageSize
            );
        }
    }
}

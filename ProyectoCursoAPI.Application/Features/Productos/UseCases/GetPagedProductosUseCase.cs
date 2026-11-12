using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Productos.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Productos.UseCases
{
    public interface IGetPagedProductosUseCase
    {
        Task<PagedResult<ProductoResponseDto>> ExecuteAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Listar productos paginados con resolucion de nombres relacionados
    public class GetPagedProductosUseCase : IGetPagedProductosUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPagedProductosUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<ProductoResponseDto>> ExecuteAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default)
        {
            var (items, totalCount) = await _unitOfWork.Productos.GetPagedAsync(
                paginationParams.PageNumber,
                paginationParams.PageSize,
                p => p.Activo,
                true,
                cancellationToken
            );

            var categorias = (await _unitOfWork.Categorias.GetAllAsync(cancellationToken)).ToDictionary(c => c.Id, c => c.Nombre);
            var proveedores = (await _unitOfWork.Proveedores.GetAllAsync(cancellationToken)).ToDictionary(p => p.Id, p => p.NombreComercial);

            var dtos = items.Select(p =>
            {
                var dto = p.ToResponseDto();
                if (categorias.TryGetValue(p.IdCategoria, out var catNombre))
                    dto.CategoriaNombre = catNombre;
                if (proveedores.TryGetValue(p.IdProveedor, out var provNombre))
                    dto.ProveedorNombreComercial = provNombre;
                return dto;
            }).ToList();

            return new PagedResult<ProductoResponseDto>(
                dtos,
                totalCount,
                paginationParams.PageNumber,
                paginationParams.PageSize
            );
        }
    }
}

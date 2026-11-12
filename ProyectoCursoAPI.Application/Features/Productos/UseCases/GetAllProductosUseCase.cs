using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Features.Productos.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Productos.UseCases
{
    public interface IGetAllProductosUseCase
    {
        Task<IEnumerable<ProductoResponseDto>> ExecuteAsync(CancellationToken cancellationToken = default);
    }

    // Caso de uso: Listar todos los productos activos
    public class GetAllProductosUseCase : IGetAllProductosUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductosUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductoResponseDto>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var productos = await _unitOfWork.Productos.FindAsync(p => p.Activo, true, cancellationToken);
            var categorias = (await _unitOfWork.Categorias.GetAllAsync(cancellationToken)).ToDictionary(c => c.Id, c => c.Nombre);
            var proveedores = (await _unitOfWork.Proveedores.GetAllAsync(cancellationToken)).ToDictionary(p => p.Id, p => p.NombreComercial);

            return productos.Select(p =>
            {
                var dto = p.ToResponseDto();
                if (categorias.TryGetValue(p.IdCategoria, out var catNombre))
                    dto.CategoriaNombre = catNombre;
                if (proveedores.TryGetValue(p.IdProveedor, out var provNombre))
                    dto.ProveedorNombreComercial = provNombre;
                return dto;
            });
        }
    }
}

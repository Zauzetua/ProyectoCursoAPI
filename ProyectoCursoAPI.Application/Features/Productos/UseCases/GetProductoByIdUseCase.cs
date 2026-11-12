using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Features.Productos.DTOs;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Productos.UseCases
{
    public interface IGetProductoByIdUseCase
    {
        Task<ProductoResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Consulta de producto por identificador con nombres de relaciones
    public class GetProductoByIdUseCase : IGetProductoByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductoByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductoResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id, cancellationToken);
            if (producto is null)
            {
                throw new NotFoundException(nameof(Producto), id);
            }

            var response = producto.ToResponseDto();

            var categoria = await _unitOfWork.Categorias.GetByIdAsync(producto.IdCategoria, cancellationToken);
            if (categoria is not null)
            {
                response.CategoriaNombre = categoria.Nombre;
            }

            var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(producto.IdProveedor, cancellationToken);
            if (proveedor is not null)
            {
                response.ProveedorNombreComercial = proveedor.NombreComercial;
            }

            return response;
        }
    }
}

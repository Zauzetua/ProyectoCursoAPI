using FluentValidation;
using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Features.Productos.DTOs;
using ProyectoCursoAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = ProyectoCursoAPI.Application.Common.Exceptions.ValidationException;

namespace ProyectoCursoAPI.Application.Features.Productos.UseCases
{
    public interface IUpdateProductoUseCase
    {
        Task<ProductoResponseDto> ExecuteAsync(UpdateProductoDto dto, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Actualizacion de producto existente
    public class UpdateProductoUseCase : IUpdateProductoUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateProductoDto> _validator;

        public UpdateProductoUseCase(IUnitOfWork unitOfWork, IValidator<UpdateProductoDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ProductoResponseDto> ExecuteAsync(UpdateProductoDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var producto = await _unitOfWork.Productos.GetByIdAsync(dto.Id, cancellationToken);
            if (producto is null)
            {
                throw new NotFoundException(nameof(Producto), dto.Id);
            }

            var categoria = await _unitOfWork.Categorias.GetByIdAsync(dto.IdCategoria, cancellationToken);
            if (categoria is null)
            {
                throw new NotFoundException(nameof(Categoria), dto.IdCategoria);
            }

            var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(dto.IdProveedor, cancellationToken);
            if (proveedor is null)
            {
                throw new NotFoundException(nameof(Proveedor), dto.IdProveedor);
            }

            producto.Actualizar(
                dto.Nombre,
                dto.Descripcion,
                dto.Precio,
                dto.Stock,
                dto.IdCategoria,
                dto.IdProveedor
            );

            _unitOfWork.Productos.Update(producto);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = producto.ToResponseDto();
            response.CategoriaNombre = categoria.Nombre;
            response.ProveedorNombreComercial = proveedor.NombreComercial;

            return response;
        }
    }
}

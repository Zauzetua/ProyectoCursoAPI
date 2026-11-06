using FluentValidation;
using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Productos.DTOs;
using ProyectoCursoAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = ProyectoCursoAPI.Application.Common.Exceptions.ValidationException;

namespace ProyectoCursoAPI.Application.Common.Services.Productos.UseCases
{
    public interface IUpdateProductoUseCase
    {
        Task<ServiceResponse<ProductoResponseDto>> ExecuteAsync(UpdateProductoDto dto, CancellationToken cancellationToken = default);
    }

    public class UpdateProductoUseCase : IUpdateProductoUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateProductoDto> _validator;

        public UpdateProductoUseCase(IUnitOfWork unitOfWork, IValidator<UpdateProductoDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ServiceResponse<ProductoResponseDto>> ExecuteAsync(UpdateProductoDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var producto = await _unitOfWork.Productos.GetByIdAsync(dto.Id);
            if (producto is null)
            {
                throw new NotFoundException(nameof(Producto), dto.Id);
            }

            if (producto.IdCategoria != dto.IdCategoria)
            {
                var categoria = await _unitOfWork.Categorias.GetByIdAsync(dto.IdCategoria);
                if (categoria is null)
                {
                    throw new NotFoundException(nameof(Categoria), dto.IdCategoria);
                }
            }

            if (producto.IdProveedor != dto.IdProveedor)
            {
                var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(dto.IdProveedor);
                if (proveedor is null)
                {
                    throw new NotFoundException(nameof(Proveedor), dto.IdProveedor);
                }
            }

            producto.Actualizar(dto.Nombre, dto.Descripcion, dto.Precio, dto.Stock, dto.IdCategoria, dto.IdProveedor);

            _unitOfWork.Productos.Update(producto);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ServiceResponse<ProductoResponseDto>.Ok(producto.ToResponseDto(), "Producto actualizado exitosamente.");
        }
    }
}

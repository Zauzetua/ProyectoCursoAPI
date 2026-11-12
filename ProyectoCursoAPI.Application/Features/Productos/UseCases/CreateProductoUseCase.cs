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
    public interface ICreateProductoUseCase
    {
        Task<ProductoResponseDto> ExecuteAsync(CreateProductoDto dto, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Creacion de un nuevo producto con validacion de entidades relacionadas
    public class CreateProductoUseCase : ICreateProductoUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProductoDto> _validator;

        public CreateProductoUseCase(IUnitOfWork unitOfWork, IValidator<CreateProductoDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ProductoResponseDto> ExecuteAsync(CreateProductoDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
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

            var producto = new Producto(
                dto.Nombre,
                dto.Descripcion,
                dto.Precio,
                dto.Stock,
                dto.IdCategoria,
                dto.IdProveedor
            );

            await _unitOfWork.Productos.AddAsync(producto, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = producto.ToResponseDto();
            response.CategoriaNombre = categoria.Nombre;
            response.ProveedorNombreComercial = proveedor.NombreComercial;

            return response;
        }
    }
}

using FluentValidation;
using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Features.Proveedores.DTOs;
using ProyectoCursoAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = ProyectoCursoAPI.Application.Common.Exceptions.ValidationException;

namespace ProyectoCursoAPI.Application.Features.Proveedores.UseCases
{
    public interface IUpdateProveedorUseCase
    {
        Task<ProveedorResponseDto> ExecuteAsync(UpdateProveedorDto dto, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Actualizacion de datos de proveedor
    public class UpdateProveedorUseCase : IUpdateProveedorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateProveedorDto> _validator;

        public UpdateProveedorUseCase(IUnitOfWork unitOfWork, IValidator<UpdateProveedorDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ProveedorResponseDto> ExecuteAsync(UpdateProveedorDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(dto.Id, cancellationToken);
            if (proveedor is null)
            {
                throw new NotFoundException(nameof(Proveedor), dto.Id);
            }

            var existeRfc = await _unitOfWork.Proveedores.GetByRFC(dto.RFC, cancellationToken);
            if (existeRfc is not null && existeRfc.Id != dto.Id)
            {
                throw new ValidationException(nameof(dto.RFC), "El RFC ya se encuentra asignado a otro proveedor.");
            }

            proveedor.Actualizar(
                dto.NombreComercial,
                dto.Direccion,
                dto.Telefono,
                dto.RFC,
                dto.Email
            );

            _unitOfWork.Proveedores.Update(proveedor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return proveedor.ToResponseDto();
        }
    }
}

using FluentValidation;
using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Proveedores.DTOs;
using ProyectoCursoAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = ProyectoCursoAPI.Application.Common.Exceptions.ValidationException;

namespace ProyectoCursoAPI.Application.Common.Services.Proveedores.UseCases
{
    public interface IUpdateProveedorUseCase
    {
        Task<ServiceResponse<ProveedorResponseDto>> ExecuteAsync(UpdateProveedorDto dto, CancellationToken cancellationToken = default);
    }

    public class UpdateProveedorUseCase : IUpdateProveedorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateProveedorDto> _validator;

        public UpdateProveedorUseCase(IUnitOfWork unitOfWork, IValidator<UpdateProveedorDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ServiceResponse<ProveedorResponseDto>> ExecuteAsync(UpdateProveedorDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(dto.Id);
            if (proveedor is null)
            {
                throw new NotFoundException(nameof(Proveedor), dto.Id);
            }

            if (proveedor.RFC != dto.RFC)
            {
                var proveedorExistente = await _unitOfWork.Proveedores.GetByRFC(dto.RFC);
                if (proveedorExistente is not null && proveedorExistente.Id != dto.Id)
                {
                    throw new ValidationException("RFC", $"Ya existe un proveedor registrado con el RFC '{dto.RFC}'.");
                }
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

            return ServiceResponse<ProveedorResponseDto>.Ok(proveedor.ToResponseDto(), "Proveedor actualizado exitosamente.");
        }
    }
}

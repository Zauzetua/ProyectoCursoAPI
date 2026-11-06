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
    public interface ICreateProveedorUseCase
    {
        Task<ServiceResponse<ProveedorResponseDto>> ExecuteAsync(CreateProveedorDto dto, CancellationToken cancellationToken = default);
    }

    public class CreateProveedorUseCase : ICreateProveedorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProveedorDto> _validator;

        public CreateProveedorUseCase(IUnitOfWork unitOfWork, IValidator<CreateProveedorDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ServiceResponse<ProveedorResponseDto>> ExecuteAsync(CreateProveedorDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var proveedorExistente = await _unitOfWork.Proveedores.GetByRFC(dto.RFC);
            if (proveedorExistente is not null)
            {
                throw new ValidationException("RFC", $"Ya existe un proveedor registrado con el RFC '{dto.RFC}'.");
            }

            var proveedor = new Proveedor(
                dto.NombreComercial,
                dto.Direccion,
                dto.Telefono,
                dto.RFC,
                dto.Email
            );

            await _unitOfWork.Proveedores.AddAsync(proveedor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ServiceResponse<ProveedorResponseDto>.Ok(proveedor.ToResponseDto(), "Proveedor creado exitosamente.");
        }
    }
}

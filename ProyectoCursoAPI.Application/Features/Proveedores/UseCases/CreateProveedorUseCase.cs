using FluentValidation;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Features.Proveedores.DTOs;
using ProyectoCursoAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = ProyectoCursoAPI.Application.Common.Exceptions.ValidationException;

namespace ProyectoCursoAPI.Application.Features.Proveedores.UseCases
{
    public interface ICreateProveedorUseCase
    {
        Task<ProveedorResponseDto> ExecuteAsync(CreateProveedorDto dto, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Creacion de un proveedor con validacion de RFC unico
    public class CreateProveedorUseCase : ICreateProveedorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProveedorDto> _validator;

        public CreateProveedorUseCase(IUnitOfWork unitOfWork, IValidator<CreateProveedorDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ProveedorResponseDto> ExecuteAsync(CreateProveedorDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existeRfc = await _unitOfWork.Proveedores.GetByRFC(dto.RFC, cancellationToken);
            if (existeRfc is not null)
            {
                throw new ValidationException(nameof(dto.RFC), "Ya existe un proveedor registrado con el mismo RFC.");
            }

            var proveedor = new Proveedor(
                dto.NombreComercial,
                dto.Direccion,
                dto.Telefono,
                dto.RFC,
                dto.Email
            );

            await _unitOfWork.Proveedores.AddAsync(proveedor, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return proveedor.ToResponseDto();
        }
    }
}

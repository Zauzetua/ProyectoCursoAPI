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
    public interface IGetProveedorByRfcUseCase
    {
        Task<ProveedorResponseDto> ExecuteAsync(string rfc, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Consulta de proveedor por su RFC
    public class GetProveedorByRfcUseCase : IGetProveedorByRfcUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProveedorByRfcUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProveedorResponseDto> ExecuteAsync(string rfc, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(rfc))
            {
                throw new ValidationException(nameof(rfc), "El RFC no puede estar vacio.");
            }

            var proveedor = await _unitOfWork.Proveedores.GetByRFC(rfc, cancellationToken);
            if (proveedor is null)
            {
                throw new NotFoundException(nameof(Proveedor), rfc);
            }

            return proveedor.ToResponseDto();
        }
    }
}

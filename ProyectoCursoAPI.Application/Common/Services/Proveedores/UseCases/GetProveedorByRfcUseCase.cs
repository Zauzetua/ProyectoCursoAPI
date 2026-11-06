using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Proveedores.DTOs;
using ProyectoCursoAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Services.Proveedores.UseCases
{
    public interface IGetProveedorByRfcUseCase
    {
        Task<ServiceResponse<ProveedorResponseDto>> ExecuteAsync(string rfc, CancellationToken cancellationToken = default);
    }

    public class GetProveedorByRfcUseCase : IGetProveedorByRfcUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProveedorByRfcUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<ProveedorResponseDto>> ExecuteAsync(string rfc, CancellationToken cancellationToken = default)
        {
            var proveedor = await _unitOfWork.Proveedores.GetByRFC(rfc);
            if (proveedor is null)
            {
                throw new NotFoundException($"Proveedor con RFC '{rfc}' no fue encontrado.");
            }

            return ServiceResponse<ProveedorResponseDto>.Ok(proveedor.ToResponseDto());
        }
    }
}

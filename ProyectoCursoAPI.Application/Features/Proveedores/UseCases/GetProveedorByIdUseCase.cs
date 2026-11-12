using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Features.Proveedores.DTOs;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Proveedores.UseCases
{
    public interface IGetProveedorByIdUseCase
    {
        Task<ProveedorResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Consulta de proveedor por su identificador
    public class GetProveedorByIdUseCase : IGetProveedorByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProveedorByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProveedorResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id, cancellationToken);
            if (proveedor is null)
            {
                throw new NotFoundException(nameof(Proveedor), id);
            }

            return proveedor.ToResponseDto();
        }
    }
}

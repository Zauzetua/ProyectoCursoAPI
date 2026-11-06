using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Proveedores.DTOs;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Services.Proveedores.UseCases
{
    public interface IGetProveedorByIdUseCase
    {
        Task<ServiceResponse<ProveedorResponseDto>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }

    public class GetProveedorByIdUseCase : IGetProveedorByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProveedorByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<ProveedorResponseDto>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id);
            if (proveedor is null)
            {
                throw new NotFoundException(nameof(Proveedor), id);
            }

            return ServiceResponse<ProveedorResponseDto>.Ok(proveedor.ToResponseDto());
        }
    }
}

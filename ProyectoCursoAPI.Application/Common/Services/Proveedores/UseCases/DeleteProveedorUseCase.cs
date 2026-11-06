using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Services.Proveedores.UseCases
{
    public interface IDeleteProveedorUseCase
    {
        Task<ServiceResponse<bool>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }

    public class DeleteProveedorUseCase : IDeleteProveedorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProveedorUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<bool>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id);
            if (proveedor is null)
            {
                throw new NotFoundException(nameof(Proveedor), id);
            }

            _unitOfWork.Proveedores.Delete(proveedor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ServiceResponse<bool>.Ok(true, "Proveedor eliminado exitosamente.");
        }
    }
}

using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Proveedores.UseCases
{
    public interface IDeleteProveedorUseCase
    {
        Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Eliminacion logica de proveedor
    public class DeleteProveedorUseCase : IDeleteProveedorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProveedorUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id, cancellationToken);
            if (proveedor is null)
            {
                throw new NotFoundException(nameof(Proveedor), id);
            }

            proveedor.Desactivar();
            _unitOfWork.Proveedores.Update(proveedor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

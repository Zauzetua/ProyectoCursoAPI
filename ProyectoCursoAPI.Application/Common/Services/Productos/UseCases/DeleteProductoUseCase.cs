using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Services.Productos.UseCases
{
    public interface IDeleteProductoUseCase
    {
        Task<ServiceResponse<bool>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }

    public class DeleteProductoUseCase : IDeleteProductoUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductoUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<bool>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto is null)
            {
                throw new NotFoundException(nameof(Producto), id);
            }

            _unitOfWork.Productos.Delete(producto);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ServiceResponse<bool>.Ok(true, "Producto eliminado exitosamente.");
        }
    }
}

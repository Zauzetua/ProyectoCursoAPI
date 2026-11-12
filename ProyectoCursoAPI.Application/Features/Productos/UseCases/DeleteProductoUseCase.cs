using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Productos.UseCases
{
    public interface IDeleteProductoUseCase
    {
        Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Eliminacion logica de producto
    public class DeleteProductoUseCase : IDeleteProductoUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductoUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id, cancellationToken);
            if (producto is null)
            {
                throw new NotFoundException(nameof(Producto), id);
            }

            producto.Desactivar();
            _unitOfWork.Productos.Update(producto);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Categorias.UseCases
{
    public interface IDeleteCategoriaUseCase
    {
        Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Eliminacion logica de una categoria
    public class DeleteCategoriaUseCase : IDeleteCategoriaUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoriaUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var categoria = await _unitOfWork.Categorias.GetByIdAsync(id, cancellationToken);
            if (categoria is null)
            {
                throw new NotFoundException(nameof(Categoria), id);
            }

            categoria.Desactivar();
            _unitOfWork.Categorias.Update(categoria);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

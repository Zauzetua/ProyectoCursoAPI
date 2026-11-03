using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Services.Categorias.UseCases
{
    public interface IDeleteCategoriaUseCase
    {
        Task<ServiceResponse<bool>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }

    public class DeleteCategoriaUseCase : IDeleteCategoriaUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoriaUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<bool>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var categoria = await _unitOfWork.Categorias.GetByIdAsync(id);
            if (categoria is null)
            {
                throw new NotFoundException(nameof(Categoria), id);
            }

            _unitOfWork.Categorias.Delete(categoria);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ServiceResponse<bool>.Ok(true, "Categoría eliminada exitosamente.");
        }
    }
}

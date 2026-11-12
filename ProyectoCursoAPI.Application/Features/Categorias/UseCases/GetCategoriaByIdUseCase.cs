using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Features.Categorias.DTOs;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Categorias.UseCases
{
    public interface IGetCategoriaByIdUseCase
    {
        Task<CategoriaResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Consulta de categoria por su ID
    public class GetCategoriaByIdUseCase : IGetCategoriaByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoriaByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoriaResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var categoria = await _unitOfWork.Categorias.GetByIdAsync(id, cancellationToken);
            if (categoria is null)
            {
                throw new NotFoundException(nameof(Categoria), id);
            }

            return categoria.ToResponseDto();
        }
    }
}

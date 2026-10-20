using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Categorias.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Services.Categorias.UseCases
{
    public interface IGetCategoriaByIdUseCase
    {
        Task<ServiceResponse<CategoriaResponseDto>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }

    public class GetCategoriaByIdUseCase : IGetCategoriaByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoriaByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<CategoriaResponseDto>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var categoria = await _unitOfWork.Categorias.GetByIdAsync(id);
            if (categoria is null)
            {
                return ServiceResponse<CategoriaResponseDto>.Fail($"No se encontró la categoría con ID '{id}'.");
            }

            return ServiceResponse<CategoriaResponseDto>.Ok(categoria.ToResponseDto());
        }
    }
}

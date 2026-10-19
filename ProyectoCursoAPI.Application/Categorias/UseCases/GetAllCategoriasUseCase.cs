using ProyectoCursoAPI.Application.Categorias.DTOs;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Categorias.UseCases
{
    public interface IGetAllCategoriasUseCase
    {
        Task<ServiceResponse<IEnumerable<CategoriaResponseDto>>> ExecuteAsync(CancellationToken cancellationToken = default);
    }

    public class GetAllCategoriasUseCase : IGetAllCategoriasUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriasUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<IEnumerable<CategoriaResponseDto>>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var categorias = await _unitOfWork.Categorias.GetAllAsync();
            var responseDtos = categorias.Select(c => c.ToResponseDto());

            return ServiceResponse<IEnumerable<CategoriaResponseDto>>.Ok(responseDtos);
        }
    }
}

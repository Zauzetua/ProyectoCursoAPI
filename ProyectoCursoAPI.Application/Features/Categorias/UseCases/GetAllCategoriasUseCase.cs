using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Features.Categorias.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Categorias.UseCases
{
    public interface IGetAllCategoriasUseCase
    {
        Task<IEnumerable<CategoriaResponseDto>> ExecuteAsync(CancellationToken cancellationToken = default);
    }

    // Caso de uso: Listar todas las categorias activas
    public class GetAllCategoriasUseCase : IGetAllCategoriasUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriasUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoriaResponseDto>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var categorias = await _unitOfWork.Categorias.FindAsync(c => c.Activo, true, cancellationToken);
            return categorias.Select(c => c.ToResponseDto());
        }
    }
}

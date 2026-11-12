using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Features.Proveedores.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Proveedores.UseCases
{
    public interface IGetAllProveedoresUseCase
    {
        Task<IEnumerable<ProveedorResponseDto>> ExecuteAsync(CancellationToken cancellationToken = default);
    }

    // Caso de uso: Listar todos los proveedores activos
    public class GetAllProveedoresUseCase : IGetAllProveedoresUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProveedoresUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProveedorResponseDto>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var proveedores = await _unitOfWork.Proveedores.FindAsync(p => p.Activo, true, cancellationToken);
            return proveedores.Select(p => p.ToResponseDto());
        }
    }
}

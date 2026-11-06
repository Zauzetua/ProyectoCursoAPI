using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Proveedores.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Services.Proveedores.UseCases
{
    public interface IGetAllProveedoresUseCase
    {
        Task<ServiceResponse<IEnumerable<ProveedorResponseDto>>> ExecuteAsync(CancellationToken cancellationToken = default);
    }

    public class GetAllProveedoresUseCase : IGetAllProveedoresUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProveedoresUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<IEnumerable<ProveedorResponseDto>>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var proveedores = await _unitOfWork.Proveedores.GetAllAsync();
            var responseDtos = proveedores.Select(p => p.ToResponseDto());

            return ServiceResponse<IEnumerable<ProveedorResponseDto>>.Ok(responseDtos);
        }
    }
}

using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Productos.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Services.Productos.UseCases
{
    public interface IGetAllProductosUseCase
    {
        Task<ServiceResponse<IEnumerable<ProductoResponseDto>>> ExecuteAsync(CancellationToken cancellationToken = default);
    }

    public class GetAllProductosUseCase : IGetAllProductosUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductosUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<IEnumerable<ProductoResponseDto>>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var productos = await _unitOfWork.Productos.GetAllAsync();
            var responseDtos = productos.Select(p => p.ToResponseDto());

            return ServiceResponse<IEnumerable<ProductoResponseDto>>.Ok(responseDtos);
        }
    }
}

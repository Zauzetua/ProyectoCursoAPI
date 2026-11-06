using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Productos.DTOs;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Services.Productos.UseCases
{
    public interface IGetProductoByIdUseCase
    {
        Task<ServiceResponse<ProductoResponseDto>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }

    public class GetProductoByIdUseCase : IGetProductoByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductoByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<ProductoResponseDto>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto is null)
            {
                throw new NotFoundException(nameof(Producto), id);
            }

            return ServiceResponse<ProductoResponseDto>.Ok(producto.ToResponseDto());
        }
    }
}

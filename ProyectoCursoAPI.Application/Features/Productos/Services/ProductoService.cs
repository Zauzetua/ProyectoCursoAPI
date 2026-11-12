using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Productos.DTOs;
using ProyectoCursoAPI.Application.Features.Productos.UseCases;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Productos.Services
{
    // Servicio integrador que orquesta los casos de uso de Producto
    public class ProductoService : IProductoService
    {
        private readonly ICreateProductoUseCase _createUseCase;
        private readonly IUpdateProductoUseCase _updateUseCase;
        private readonly IGetProductoByIdUseCase _getByIdUseCase;
        private readonly IGetAllProductosUseCase _getAllUseCase;
        private readonly IGetPagedProductosUseCase _getPagedUseCase;
        private readonly IDeleteProductoUseCase _deleteUseCase;

        public ProductoService(
            ICreateProductoUseCase createUseCase,
            IUpdateProductoUseCase updateUseCase,
            IGetProductoByIdUseCase getByIdUseCase,
            IGetAllProductosUseCase getAllUseCase,
            IGetPagedProductosUseCase getPagedUseCase,
            IDeleteProductoUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _updateUseCase = updateUseCase;
            _getByIdUseCase = getByIdUseCase;
            _getAllUseCase = getAllUseCase;
            _getPagedUseCase = getPagedUseCase;
            _deleteUseCase = deleteUseCase;
        }

        public Task<ProductoResponseDto> CreateAsync(CreateProductoDto dto, CancellationToken cancellationToken = default)
            => _createUseCase.ExecuteAsync(dto, cancellationToken);

        public Task<ProductoResponseDto> UpdateAsync(UpdateProductoDto dto, CancellationToken cancellationToken = default)
            => _updateUseCase.ExecuteAsync(dto, cancellationToken);

        public Task<ProductoResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => _getByIdUseCase.ExecuteAsync(id, cancellationToken);

        public Task<IEnumerable<ProductoResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
            => _getAllUseCase.ExecuteAsync(cancellationToken);

        public Task<PagedResult<ProductoResponseDto>> GetPagedAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default)
            => _getPagedUseCase.ExecuteAsync(paginationParams, cancellationToken);

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
            => _deleteUseCase.ExecuteAsync(id, cancellationToken);
    }
}

using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Categorias.DTOs;
using ProyectoCursoAPI.Application.Features.Categorias.UseCases;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Categorias.Services
{
    // Servicio integrador que orquesta los casos de uso de Categoria.
    public class CategoriaService : ICategoriaService
    {
        private readonly ICreateCategoriaUseCase _createUseCase;
        private readonly IUpdateCategoriaUseCase _updateUseCase;
        private readonly IGetCategoriaByIdUseCase _getByIdUseCase;
        private readonly IGetAllCategoriasUseCase _getAllUseCase;
        private readonly IGetPagedCategoriasUseCase _getPagedUseCase;
        private readonly IDeleteCategoriaUseCase _deleteUseCase;

        public CategoriaService(
            ICreateCategoriaUseCase createUseCase,
            IUpdateCategoriaUseCase updateUseCase,
            IGetCategoriaByIdUseCase getByIdUseCase,
            IGetAllCategoriasUseCase getAllUseCase,
            IGetPagedCategoriasUseCase getPagedUseCase,
            IDeleteCategoriaUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _updateUseCase = updateUseCase;
            _getByIdUseCase = getByIdUseCase;
            _getAllUseCase = getAllUseCase;
            _getPagedUseCase = getPagedUseCase;
            _deleteUseCase = deleteUseCase;
        }

        public Task<CategoriaResponseDto> CreateAsync(CreateCategoriaDto dto, CancellationToken cancellationToken = default)
            => _createUseCase.ExecuteAsync(dto, cancellationToken);

        public Task<CategoriaResponseDto> UpdateAsync(UpdateCategoriaDto dto, CancellationToken cancellationToken = default)
            => _updateUseCase.ExecuteAsync(dto, cancellationToken);

        public Task<CategoriaResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => _getByIdUseCase.ExecuteAsync(id, cancellationToken);

        public Task<IEnumerable<CategoriaResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
            => _getAllUseCase.ExecuteAsync(cancellationToken);

        public Task<PagedResult<CategoriaResponseDto>> GetPagedAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default)
            => _getPagedUseCase.ExecuteAsync(paginationParams, cancellationToken);

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
            => _deleteUseCase.ExecuteAsync(id, cancellationToken);
    }
}

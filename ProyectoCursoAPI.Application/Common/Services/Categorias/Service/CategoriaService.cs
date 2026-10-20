using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Categorias.DTOs;
using ProyectoCursoAPI.Application.Common.Services.Categorias.UseCases;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Services.Categorias.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICreateCategoriaUseCase _createUseCase;
        private readonly IUpdateCategoriaUseCase _updateUseCase;
        private readonly IGetCategoriaByIdUseCase _getByIdUseCase;
        private readonly IGetAllCategoriasUseCase _getAllUseCase;
        private readonly IDeleteCategoriaUseCase _deleteUseCase;

        public CategoriaService(
            ICreateCategoriaUseCase createUseCase,
            IUpdateCategoriaUseCase updateUseCase,
            IGetCategoriaByIdUseCase getByIdUseCase,
            IGetAllCategoriasUseCase getAllUseCase,
            IDeleteCategoriaUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _updateUseCase = updateUseCase;
            _getByIdUseCase = getByIdUseCase;
            _getAllUseCase = getAllUseCase;
            _deleteUseCase = deleteUseCase;
        }

        public Task<ServiceResponse<CategoriaResponseDto>> CreateAsync(CreateCategoriaDto dto, CancellationToken cancellationToken = default)
            => _createUseCase.ExecuteAsync(dto, cancellationToken);

        public Task<ServiceResponse<CategoriaResponseDto>> UpdateAsync(UpdateCategoriaDto dto, CancellationToken cancellationToken = default)
            => _updateUseCase.ExecuteAsync(dto, cancellationToken);

        public Task<ServiceResponse<CategoriaResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => _getByIdUseCase.ExecuteAsync(id, cancellationToken);

        public Task<ServiceResponse<IEnumerable<CategoriaResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
            => _getAllUseCase.ExecuteAsync(cancellationToken);

        public Task<ServiceResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
            => _deleteUseCase.ExecuteAsync(id, cancellationToken);
    }
}

using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Proveedores.DTOs;
using ProyectoCursoAPI.Application.Features.Proveedores.UseCases;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Features.Proveedores.Services
{
    // Servicio integrador que orquesta los casos de uso de Proveedor
    public class ProveedorService : IProveedorService
    {
        private readonly ICreateProveedorUseCase _createUseCase;
        private readonly IUpdateProveedorUseCase _updateUseCase;
        private readonly IGetProveedorByIdUseCase _getByIdUseCase;
        private readonly IGetProveedorByRfcUseCase _getByRfcUseCase;
        private readonly IGetAllProveedoresUseCase _getAllUseCase;
        private readonly IGetPagedProveedoresUseCase _getPagedUseCase;
        private readonly IDeleteProveedorUseCase _deleteUseCase;

        public ProveedorService(
            ICreateProveedorUseCase createUseCase,
            IUpdateProveedorUseCase updateUseCase,
            IGetProveedorByIdUseCase getByIdUseCase,
            IGetProveedorByRfcUseCase getByRfcUseCase,
            IGetAllProveedoresUseCase getAllUseCase,
            IGetPagedProveedoresUseCase getPagedUseCase,
            IDeleteProveedorUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _updateUseCase = updateUseCase;
            _getByIdUseCase = getByIdUseCase;
            _getByRfcUseCase = getByRfcUseCase;
            _getAllUseCase = getAllUseCase;
            _getPagedUseCase = getPagedUseCase;
            _deleteUseCase = deleteUseCase;
        }

        public Task<ProveedorResponseDto> CreateAsync(CreateProveedorDto dto, CancellationToken cancellationToken = default)
            => _createUseCase.ExecuteAsync(dto, cancellationToken);

        public Task<ProveedorResponseDto> UpdateAsync(UpdateProveedorDto dto, CancellationToken cancellationToken = default)
            => _updateUseCase.ExecuteAsync(dto, cancellationToken);

        public Task<ProveedorResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => _getByIdUseCase.ExecuteAsync(id, cancellationToken);

        public Task<ProveedorResponseDto> GetByRfcAsync(string rfc, CancellationToken cancellationToken = default)
            => _getByRfcUseCase.ExecuteAsync(rfc, cancellationToken);

        public Task<IEnumerable<ProveedorResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
            => _getAllUseCase.ExecuteAsync(cancellationToken);

        public Task<PagedResult<ProveedorResponseDto>> GetPagedAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default)
            => _getPagedUseCase.ExecuteAsync(paginationParams, cancellationToken);

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
            => _deleteUseCase.ExecuteAsync(id, cancellationToken);
    }
}

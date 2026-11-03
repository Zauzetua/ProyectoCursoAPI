using FluentValidation;
using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Categorias.DTOs;
using ProyectoCursoAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = ProyectoCursoAPI.Application.Common.Exceptions.ValidationException;

namespace ProyectoCursoAPI.Application.Common.Services.Categorias.UseCases
{
    public interface IUpdateCategoriaUseCase
    {
        Task<ServiceResponse<CategoriaResponseDto>> ExecuteAsync(UpdateCategoriaDto dto, CancellationToken cancellationToken = default);
    }

    public class UpdateCategoriaUseCase : IUpdateCategoriaUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateCategoriaDto> _validator;

        public UpdateCategoriaUseCase(IUnitOfWork unitOfWork, IValidator<UpdateCategoriaDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ServiceResponse<CategoriaResponseDto>> ExecuteAsync(UpdateCategoriaDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var categoria = await _unitOfWork.Categorias.GetByIdAsync(dto.Id);
            if (categoria is null)
            {
                throw new NotFoundException(nameof(Categoria), dto.Id);
            }

            categoria.Actualizar(dto.Nombre, dto.Descripcion ?? string.Empty);

            _unitOfWork.Categorias.Update(categoria);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ServiceResponse<CategoriaResponseDto>.Ok(categoria.ToResponseDto(), "Categoría actualizada exitosamente.");
        }
    }
}

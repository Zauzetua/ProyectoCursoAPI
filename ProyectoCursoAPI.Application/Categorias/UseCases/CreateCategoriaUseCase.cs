using FluentValidation;
using ProyectoCursoAPI.Application.Categorias.DTOs;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Categorias.UseCases
{
    public interface ICreateCategoriaUseCase
    {
        Task<ServiceResponse<CategoriaResponseDto>> ExecuteAsync(CreateCategoriaDto dto, CancellationToken cancellationToken = default);
    }

    public class CreateCategoriaUseCase : ICreateCategoriaUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateCategoriaDto> _validator;

        public CreateCategoriaUseCase(IUnitOfWork unitOfWork, IValidator<CreateCategoriaDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ServiceResponse<CategoriaResponseDto>> ExecuteAsync(CreateCategoriaDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return ServiceResponse<CategoriaResponseDto>.Fail(errors);
            }

            var categoria = new Categoria(dto.Nombre, dto.Descripcion ?? string.Empty);

            await _unitOfWork.Categorias.AddAsync(categoria);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ServiceResponse<CategoriaResponseDto>.Ok(categoria.ToResponseDto(), "Categoría creada exitosamente.");
        }
    }
}

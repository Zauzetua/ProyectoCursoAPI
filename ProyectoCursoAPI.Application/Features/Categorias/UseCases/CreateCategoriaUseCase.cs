using FluentValidation;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Features.Categorias.DTOs;
using ProyectoCursoAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = ProyectoCursoAPI.Application.Common.Exceptions.ValidationException;

namespace ProyectoCursoAPI.Application.Features.Categorias.UseCases
{
    public interface ICreateCategoriaUseCase
    {
        Task<CategoriaResponseDto> ExecuteAsync(CreateCategoriaDto dto, CancellationToken cancellationToken = default);
    }

    // Caso de uso: Creacion de una categoria
    public class CreateCategoriaUseCase : ICreateCategoriaUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateCategoriaDto> _validator;

        public CreateCategoriaUseCase(IUnitOfWork unitOfWork, IValidator<CreateCategoriaDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<CategoriaResponseDto> ExecuteAsync(CreateCategoriaDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var categoria = new Categoria(dto.Nombre, dto.Descripcion);

            await _unitOfWork.Categorias.AddAsync(categoria, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return categoria.ToResponseDto();
        }
    }
}

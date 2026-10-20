using FluentValidation;

namespace ProyectoCursoAPI.Application.Common.Services.Categorias.DTOs
{
    public record CreateCategoriaDto(string Nombre, string? Descripcion);

    public class CreateCategoriaValidator : AbstractValidator<CreateCategoriaDto>
    {
        public CreateCategoriaValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(200).WithMessage("La descripción no puede exceder los 200 caracteres.");
        }
    }
}

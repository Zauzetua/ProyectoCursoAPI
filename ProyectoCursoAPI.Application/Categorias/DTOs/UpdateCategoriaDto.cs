using FluentValidation;
using System;

namespace ProyectoCursoAPI.Application.Categorias.DTOs
{
    public record UpdateCategoriaDto(Guid Id, string Nombre, string? Descripcion);

    public class UpdateCategoriaValidator : AbstractValidator<UpdateCategoriaDto>
    {
        public UpdateCategoriaValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El identificador de la categoría es obligatorio.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(200).WithMessage("La descripción no puede exceder los 200 caracteres.");
        }
    }
}

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Services.Categorias.Services;
using ProyectoCursoAPI.Application.Common.Services.Categorias.UseCases;
using System.Reflection;

namespace ProyectoCursoAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // FluentValidation: registra todos los validadores del ensamblado
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Use Cases
            services.AddScoped<ICreateCategoriaUseCase, CreateCategoriaUseCase>();
            services.AddScoped<IUpdateCategoriaUseCase, UpdateCategoriaUseCase>();
            services.AddScoped<IGetCategoriaByIdUseCase, GetCategoriaByIdUseCase>();
            services.AddScoped<IGetAllCategoriasUseCase, GetAllCategoriasUseCase>();
            services.AddScoped<IDeleteCategoriaUseCase, DeleteCategoriaUseCase>();

            // Servicio integrador
            services.AddScoped<ICategoriaService, CategoriaService>();

            return services;
        }
    }
}

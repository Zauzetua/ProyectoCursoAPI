using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Services.Categorias.Services;
using ProyectoCursoAPI.Application.Common.Services.Categorias.UseCases;
using ProyectoCursoAPI.Application.Common.Services.Productos.Services;
using ProyectoCursoAPI.Application.Common.Services.Productos.UseCases;
using ProyectoCursoAPI.Application.Common.Services.Proveedores.Services;
using ProyectoCursoAPI.Application.Common.Services.Proveedores.UseCases;
using System.Reflection;

namespace ProyectoCursoAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // FluentValidation: registra todos los validadores del ensamblado
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Use Cases - Categorias
            services.AddScoped<ICreateCategoriaUseCase, CreateCategoriaUseCase>();
            services.AddScoped<IUpdateCategoriaUseCase, UpdateCategoriaUseCase>();
            services.AddScoped<IGetCategoriaByIdUseCase, GetCategoriaByIdUseCase>();
            services.AddScoped<IGetAllCategoriasUseCase, GetAllCategoriasUseCase>();
            services.AddScoped<IGetPagedCategoriasUseCase, GetPagedCategoriasUseCase>();
            services.AddScoped<IDeleteCategoriaUseCase, DeleteCategoriaUseCase>();

            // Use Cases - Productos
            services.AddScoped<ICreateProductoUseCase, CreateProductoUseCase>();
            services.AddScoped<IUpdateProductoUseCase, UpdateProductoUseCase>();
            services.AddScoped<IGetProductoByIdUseCase, GetProductoByIdUseCase>();
            services.AddScoped<IGetAllProductosUseCase, GetAllProductosUseCase>();
            services.AddScoped<IGetPagedProductosUseCase, GetPagedProductosUseCase>();
            services.AddScoped<IDeleteProductoUseCase, DeleteProductoUseCase>();

            // Use Cases - Proveedores
            services.AddScoped<ICreateProveedorUseCase, CreateProveedorUseCase>();
            services.AddScoped<IUpdateProveedorUseCase, UpdateProveedorUseCase>();
            services.AddScoped<IGetProveedorByIdUseCase, GetProveedorByIdUseCase>();
            services.AddScoped<IGetProveedorByRfcUseCase, GetProveedorByRfcUseCase>();
            services.AddScoped<IGetAllProveedoresUseCase, GetAllProveedoresUseCase>();
            services.AddScoped<IGetPagedProveedoresUseCase, GetPagedProveedoresUseCase>();
            services.AddScoped<IDeleteProveedorUseCase, DeleteProveedorUseCase>();

            // Servicios integradores
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IProveedorService, ProveedorService>();

            return services;
        }
    }
}

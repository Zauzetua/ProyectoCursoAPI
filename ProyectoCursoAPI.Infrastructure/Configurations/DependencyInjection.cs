using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Infrastructure.Data;
using ProyectoCursoAPI.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoCursoAPI.Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuro mi cadena de conexion optimizada
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            AddRepositories(services);
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            //Aqui van los repositorios de la capa de infraestructura.
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProveedorRepository, ProveedorRepository>();
            //services.AddScoped<IProductoRepository, ProductoRepository>();
            //services.AddScoped<ICategoriaRepository, CategoriaRepository>();

        }
    }
}
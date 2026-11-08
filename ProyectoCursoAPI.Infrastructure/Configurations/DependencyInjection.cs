using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Infrastructure.Data;
using ProyectoCursoAPI.Infrastructure.Repositories;
using ProyectoCursoAPI.Infrastructure.Services;

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

            // Configuración de ASP.NET Core Identity (API / Tokens)
            services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            // Servicios de infraestructura para seguridad y JWT
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAuthService, AuthService>();

            AddRepositories(services);
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            //Aqui van los repositorios de la capa de infraestructura.
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProveedorRepository, ProveedorRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

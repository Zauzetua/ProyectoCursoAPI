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
    // Configuracion de dependencias de la capa de infraestructura
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Conexion a la base de datos con SQL Server
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Configuracion de ASP.NET Core Identity completo (incluye SignInManager, Roles y Cookies)
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Configuracion de la cookie de autenticacion
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
            });

            // Servicio de autenticacion de infraestructura basado en Identity
            services.AddScoped<IAuthService, AuthService>();

            AddRepositories(services);
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProveedorRepository, ProveedorRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

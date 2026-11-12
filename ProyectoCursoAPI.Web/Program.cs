using Microsoft.EntityFrameworkCore;
using ProyectoCursoAPI.Application;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Infrastructure.Configurations;
using ProyectoCursoAPI.Infrastructure.Data;
using ProyectoCursoAPI.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Servicios de Infrastructure y Application
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Servicio de contexto HTTP y usuario actual (Auditoria)
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// Soporte completo para MVC con Vistas y validacion antiforgery global
builder.Services.AddControllersWithViews(options =>
{
    // AutoValidateAntiforgeryToken asegura proteccion CSRF en peticiones POST, PUT y DELETE
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute());
});

var app = builder.Build();

// Aplicacion automatica de migraciones pendientes de EF Core al iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrio un error al aplicar las migraciones a la base de datos.");
    }
}

// Manejo de errores amigable en produccion
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePagesWithReExecute("/Home/NotFoundPage");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Middleware de autenticacion y autorizacion basado en cookies
app.UseAuthentication();
app.UseAuthorization();

// Rutas convencionales de ASP.NET Core MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

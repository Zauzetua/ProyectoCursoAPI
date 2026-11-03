using Microsoft.EntityFrameworkCore;
using ProyectoCursoAPI.Infrastructure.Data;
using System.Runtime.CompilerServices;
using ProyectoCursoAPI.Application;
using Microsoft.OpenApi;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API CURSO",
        Description = "Documentación interactiva de endpoints y API"
    });
});
//Configuracion de Infrastructure
ProyectoCursoAPI.Infrastructure.Configurations.DependencyInjection.AddInfrastructureServices(builder.Services, builder.Configuration);
//COnfiguracion de Application
ProyectoCursoAPI.Application.DependencyInjection.AddApplicationServices(builder.Services);

builder.Services.AddControllers();




var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");

        // Carga Swagger UI directamente en la raiz de la aplicacion
        options.RoutePrefix = string.Empty;
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

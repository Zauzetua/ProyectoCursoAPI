using Microsoft.EntityFrameworkCore;
using ProyectoCursoAPI.Infrastructure.Data;
using System.Runtime.CompilerServices;
using ProyectoCursoAPI.Application;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuracion de Infrastructure
ProyectoCursoAPI.Infrastructure.Configurations.DependencyInjection.AddInfrastructureServices(builder.Services, builder.Configuration);
//COnfiguracion de Application
ProyectoCursoAPI.Application.DependencyInjection.AddApplicationServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

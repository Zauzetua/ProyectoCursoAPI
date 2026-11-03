#!/bin/bash
# Script para aplicar migraciones a la base de datos de forma rapida
dotnet ef database update --project ProyectoCursoAPI.Infrastructure/ProyectoCursoAPI.Infrastructure.csproj --startup-project ProyectoCursoAPI.Web/ProyectoCursoAPI.Web.csproj

#!/bin/bash
# Uso: ./add-migration.sh NombreDeTuMigracion
dotnet ef migrations add $1 --project ProyectoCursoAPI.Infrastructure/ProyectoCursoAPI.Infrastructure.csproj --startup-project ProyectoCursoAPI.Web/ProyectoCursoAPI.Web.csproj
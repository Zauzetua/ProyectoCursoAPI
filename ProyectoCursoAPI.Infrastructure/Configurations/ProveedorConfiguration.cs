using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoCursoAPI.Infrastructure.Configurations
{
    internal sealed class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.ToTable("Proveedor"); // Nombre de la tabla

            builder.HasKey(p => p.Id); // Clave primaria

            builder.Property(p => p.NombreComercial)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Direccion)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Telefono)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(p => p.Productos)
                .WithOne(p => p.Proveedor)
                .HasForeignKey(p => p.IdProveedor)
                .OnDelete(DeleteBehavior.Restrict); // Configuración de la relacion con Producto
        }
    }
}

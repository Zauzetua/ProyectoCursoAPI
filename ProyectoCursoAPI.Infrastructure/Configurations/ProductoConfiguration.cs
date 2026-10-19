using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoCursoAPI.Infrastructure.Configurations
{
    internal sealed class ProductoConfiguration : BaseEntityConfiguration<Producto>
    {
        public override void Configure(EntityTypeBuilder<Producto> builder)
        {
            base.Configure(builder);
            builder.ToTable("Producto"); // Nombre de la tabla

            builder.HasKey(p => p.Id); // Clave primaria

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Descripcion)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Precio)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Stock)
                .IsRequired();

            builder.HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.IdCategoria);

            builder.HasOne(p => p.Proveedor)
                .WithMany(pr => pr.Productos)
                .HasForeignKey(p => p.IdProveedor);
        }
    }
}

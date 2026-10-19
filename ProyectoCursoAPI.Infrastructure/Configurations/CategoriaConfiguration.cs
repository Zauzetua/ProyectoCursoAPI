using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoCursoAPI.Infrastructure.Configurations
{
    internal sealed class CategoriaConfiguration : BaseEntityConfiguration<Categoria>
    {
        public override void Configure(EntityTypeBuilder<Categoria> builder)
        {
            base.Configure(builder);
            builder.ToTable("Categoria"); // Nombre de la tabla

            builder.HasKey(c => c.Id); // Clave primaria

            builder.Property(c => c.Nombre)
                   .IsRequired()
                   .HasMaxLength(100); // Nombre es requerido y tiene un maximo de 100 caracteres

            builder.Property(c => c.Descripcion)
                   .HasMaxLength(200); // Descripción es opcional y tiene un maximo de 200 caracteres

        }
    }
}

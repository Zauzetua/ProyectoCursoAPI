using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoCursoAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoCursoAPI.Infrastructure.Configurations
{
    internal abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {//Toda esta configuracion es para que se pueda mapear la entidad base y sus propiedades en la base de datos
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FechaCreacion)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            //Id usuario de Identity
            builder.Property(e => e.UsuarioAlta)
                .IsRequired()
                .HasColumnType("nvarchar(450)");

            builder.Property(e => e.FechaModificacion);
            builder.Property(e => e.UsuarioModificacion)
                .HasColumnType("nvarchar(450)");

            builder.Property(e => e.FechaBaja);
            builder.Property(e => e.UsuarioBaja)
                .HasColumnType("nvarchar(450)");

            builder.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}

using System;

namespace ProyectoCursoAPI.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string UsuarioAlta { get; protected set; } = string.Empty;
        public string? UsuarioModificacion { get; protected set; }
        public string? UsuarioBaja { get; protected set; }
        public DateTime FechaCreacion { get; protected set; } = DateTime.UtcNow;
        public DateTime? FechaModificacion { get; protected set; }
        public DateTime? FechaBaja { get; protected set; }
        public bool Activo { get; protected set; } = true;

        public virtual void Desactivar(string? usuario = null)
        {
            Activo = false;
            FechaBaja = DateTime.UtcNow;
            UsuarioBaja = usuario;
        }

        public virtual void Activar()
        {
            Activo = true;
            FechaBaja = null;
            UsuarioBaja = null;
        }

        public virtual void SetAuditoriaCreacion(string usuario)
        {
            UsuarioAlta = usuario;
            FechaCreacion = DateTime.UtcNow;
            Activo = true;
        }

        public virtual void SetAuditoriaModificacion(string usuario)
        {
            UsuarioModificacion = usuario;
            FechaModificacion = DateTime.UtcNow;
        }
    }
}

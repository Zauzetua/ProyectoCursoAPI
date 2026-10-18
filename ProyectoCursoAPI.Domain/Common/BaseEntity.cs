using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoCursoAPI.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string UsuarioAlta { get; protected set; } = string.Empty;
        public string? UsuarioModificacion { get; protected set; } = string.Empty;
        public string? UsuarioBaja { get; protected set; } = string.Empty;
        public DateTime FechaCreacion { get; init; } = DateTime.UtcNow;
        public DateTime? FechaModificacion { get; protected set; }
        public DateTime? FechaBaja { get; protected set; }
        public bool Activo { get; protected set; } = true;
    }
}

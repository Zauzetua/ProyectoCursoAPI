using ProyectoCursoAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoCursoAPI.Domain.Entities
{
    public class Proveedor : BaseEntity
    {
        public string NombreComercial { get; private set; } = string.Empty;
        public string Direccion { get; private set; } = string.Empty;
        public string Telefono { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        //Propiedad de navegacion para la relacion uno a muchos con Producto.
        public virtual ICollection<Producto> Productos { get; private set; } = [];
    }
}

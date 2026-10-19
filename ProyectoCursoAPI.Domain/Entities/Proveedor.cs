using ProyectoCursoAPI.Domain.Common;
using ProyectoCursoAPI.Domain.Exceptions;
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
        public string RFC { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        //Propiedad de navegacion para la relacion uno a muchos con Producto.
        public virtual ICollection<Producto> Productos { get; private set; } = [];

        private Proveedor() { } // Constructor privado para EF Core 

        public Proveedor(string nombreComercial, string direccion, string telefono, string rfc, string email)
        {
            ValidarRFC(rfc);
            ValidarTelefono(telefono);
            NombreComercial = nombreComercial;
            Direccion = direccion;
            Telefono = telefono;
            RFC = rfc;
            Email = email;
        }

        private static void ValidarTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                throw new DomainException("El teléfono del proveedor es obligatorio.");
            if (telefono.Length < 10 || telefono.Length > 15)
                throw new DomainException("El teléfono del proveedor debe tener entre 10 y 15 caracteres.");
            //Se simula que se valida el telefono.
        }

        private static void ValidarRFC(string rfc)
        {
            if (string.IsNullOrWhiteSpace(rfc))
                throw new DomainException("El RFC del proveedor es obligatorio.");
            if (rfc.Length != 12 && rfc.Length != 13)
                throw new DomainException("El RFC del proveedor debe tener 12 o 13 caracteres.");
            //Se simula que se valida el RFC.
        }



    }
}

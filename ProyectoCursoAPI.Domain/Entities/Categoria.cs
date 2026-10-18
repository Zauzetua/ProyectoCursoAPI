using ProyectoCursoAPI.Domain.Common;
using ProyectoCursoAPI.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoCursoAPI.Domain.Entities
{
    public class Categoria : BaseEntity
    {
        public string Nombre { get; private set; } = string.Empty;
        public string? Descripcion { get; private set; } = string.Empty;
        //Propiedad de navegacion para la relación uno a muchos con Producto
        public virtual ICollection<Producto> Productos { get; private set; } = [];

        private Categoria() { } // Constructor privado para EF Core

        public Categoria(string nombre, string descripcion)
        {
            ValidarDatos(nombre);
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public void Actualizar(string nombre, string descripcion)
        {
            ValidarDatos(nombre);
            Nombre = nombre;
            Descripcion = descripcion;
        }

        private static void ValidarDatos(string nombre)
        {

            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre de la categoria es obligatorio.");
        }

    }
}

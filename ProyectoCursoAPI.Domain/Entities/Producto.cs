using ProyectoCursoAPI.Domain.Common;
using ProyectoCursoAPI.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoCursoAPI.Domain.Entities
{
    public class Producto : BaseEntity
    {
        public string Nombre { get; private set; } = string.Empty;
        public string Descripcion { get; private set; } = string.Empty;
        public decimal Precio { get; private set; }
        public int Stock { get; private set; }

        //Este es el Id de la categoria a la que pertenece el producto, es una clave foranea.
        public Guid IdCategoria { get; private set; }
        //Propiedad de navegacion para la relacion muchos a uno con Categoria.
        public virtual Categoria? Categoria { get; private set; }

        //Este es el Id del proveedor al que pertenece el producto, es una clave foranea.
        public Guid IdProveedor { get; private set; }
        //Propiedad de navegacion para la relacion muchos a uno con Proveedor.
        public virtual Proveedor Proveedor { get; private set; } = new Proveedor();

        private Producto() { } // Constructor privado para EF Core

        public Producto(string nombre, string descripcion, decimal precio, int stock, Guid idCategoria, Guid idProveedor)
        {
            ValidarNombre(nombre);
            ValidarPrecio(precio);
            ValidarStock(stock);

            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            IdCategoria = idCategoria;
            IdProveedor = idProveedor;
        }

        public void Actualizar(string nombre, string descripcion, decimal precio, Guid idCategoria, Guid idProveedor)
        {
            ValidarNombre(nombre);
            ValidarPrecio(precio);
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            IdCategoria = idCategoria;
            IdProveedor = idProveedor;
        }

        private static void ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre del producto es obligatorio.");
        }

        private static void ValidarPrecio(decimal precio)
        {
            if (precio < 0)
                throw new DomainException("El precio del producto no puede ser negativo.");
        }

        private static void ValidarStock(int stock)
        {
            if (stock < 0)
                throw new DomainException("El stock del producto no puede ser negativo.");
        }

        public void AjustarStock(int cantidad)
        {
            var nuevoStock = Stock + cantidad;
            ValidarStock(nuevoStock);
            Stock = nuevoStock;
        }
    }
}

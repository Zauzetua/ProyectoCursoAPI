using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoCursoAPI.Domain.Exceptions
{
    /// <summary>
    /// Excepcion que se lanza cuando ocurre un error en la capa de dominio.
    /// </summary>
    public class DomainException : Exception
    {
        //Haciendo esto, hacemos una excepcion personalizada.
        public DomainException(string message) : base(message)
        {
        }
    }
}

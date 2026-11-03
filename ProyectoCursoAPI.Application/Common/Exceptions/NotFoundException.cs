using System;

namespace ProyectoCursoAPI.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string entityName, object key)
            : base($"El recurso '{entityName}' con identificador '{key}' no fue encontrado.")
        {
        }
    }
}

using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Repositories
{
    public interface IProveedorRepository : IGenericRepository<Proveedor>
    {
        Task<Proveedor?> GetByRFC(string rfc);

    }
}

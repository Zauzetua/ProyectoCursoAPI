using ProyectoCursoAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Repositories
{
    // Repositorio especializado para Proveedor con operaciones especificas.
    public interface IProveedorRepository : IGenericRepository<Proveedor>
    {
        Task<Proveedor?> GetByRFC(string rfc, CancellationToken cancellationToken = default);
    }
}

using Microsoft.EntityFrameworkCore;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Domain.Entities;
using ProyectoCursoAPI.Infrastructure.Data;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Infrastructure.Repositories
{
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedorRepository
    {
        public ProveedorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Proveedor?> GetByRFC(string rfc)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.RFC == rfc);
        }
    }
}

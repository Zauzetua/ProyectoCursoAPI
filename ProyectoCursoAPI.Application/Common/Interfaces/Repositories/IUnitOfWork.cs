using ProyectoCursoAPI.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Producto> Productos { get; }
        IGenericRepository<Categoria> Categorias { get; }
        IProveedorRepository Proveedores { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task<bool> CommitAsync();
        Task RollbackAsync();
    }
}

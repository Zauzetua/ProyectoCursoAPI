using ProyectoCursoAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true);
        Task<(IReadOnlyList<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null, bool asNoTracking = true);
    }
}

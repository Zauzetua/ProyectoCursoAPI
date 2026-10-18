using ProyectoCursoAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Repositories
{
    internal interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);

    }
}

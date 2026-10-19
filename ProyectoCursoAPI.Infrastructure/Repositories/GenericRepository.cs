using Microsoft.EntityFrameworkCore;
using ProyectoCursoAPI.Application.Common.Interfaces.Repositories;
using ProyectoCursoAPI.Domain.Common;
using ProyectoCursoAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ProyectoCursoAPI.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Esto representa el contexto de la base de datos, que es responsable de interactuar con la base de datos y realizar operaciones CRUD.
        /// </summary>
        protected readonly ApplicationDbContext _context;
        /// <summary>
        /// Este repreenta el conjunto de entidades del tipo T en la base de datos. Es utilizado para realizar operaciones específicas en la entidad T.
        /// </summary>
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            //No hacemos save, porque el UnitOfWork se encargara de eso
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true)
        {
            if (asNoTracking)
            {
                return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
            }
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}

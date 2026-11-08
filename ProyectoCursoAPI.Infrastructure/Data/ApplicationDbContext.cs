using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Domain.Common;
using ProyectoCursoAPI.Domain.Entities;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly ICurrentUserService? _currentUserService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService? currentUserService = null) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Producto> Producto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuración de las entidades
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInformation();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            ApplyAuditInformation();
            return base.SaveChanges();
        }

        private void ApplyAuditInformation()
        {
            var currentUser = _currentUserService?.UserId
                ?? _currentUserService?.UserName
                ?? "Sistema";

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        var usuarioAlta = string.IsNullOrWhiteSpace(entry.Entity.UsuarioAlta)
                            ? currentUser
                            : entry.Entity.UsuarioAlta;
                        entry.Entity.SetAuditoriaCreacion(usuarioAlta);
                        break;

                    case EntityState.Modified:
                        var usuarioMod = string.IsNullOrWhiteSpace(entry.Entity.UsuarioModificacion)
                            ? currentUser
                            : entry.Entity.UsuarioModificacion;
                        entry.Entity.SetAuditoriaModificacion(usuarioMod);
                        break;

                    case EntityState.Deleted:
                        // Convierte borrado físico en borrado lógico (Soft Delete) automáticamente
                        entry.State = EntityState.Modified;
                        var usuarioBaja = string.IsNullOrWhiteSpace(entry.Entity.UsuarioBaja)
                            ? currentUser
                            : entry.Entity.UsuarioBaja;
                        entry.Entity.Desactivar(usuarioBaja);
                        break;
                }
            }
        }
    }
}

using CentroEventos.Aplicacion.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Aplicacion
{
    public class CentroEventosContext : DbContext
    {
        public CentroEventosContext(DbContextOptions<CentroEventosContext> options)
            : base(options)
        {
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<EventoDeportivo> EventosDeportivos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          base.OnModelCreating(modelBuilder);

          modelBuilder.Entity<UsuarioPermiso>()
            .HasKey(up => new { up.UsuarioId, up.Permiso }); // clave compuesta

          modelBuilder.Entity<UsuarioPermiso>()
            .HasOne(up => up.Usuario)
            .WithMany(u => u.UsuarioPermisos)
            .HasForeignKey(up => up.UsuarioId);
        }
    }
}
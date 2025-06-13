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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Permisos)
                .HasConversion<int>(); // guarda los flags como int

            base.OnModelCreating(modelBuilder);
        }
    }
}
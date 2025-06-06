using Microsoft.EntityFrameworkCore;
using CentroEventos.Aplicacion.entidades;

namespace CentroEventos.Aplicacion.Data
{
    public class CentroEventosContext : DbContext
    {
        public CentroEventosContext(DbContextOptions<CentroEventosContext> options)
            : base(options)
        {
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<EventoDeportivo> EventosDeportivos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=CentroEventos.sqlite");
        }
    }
}

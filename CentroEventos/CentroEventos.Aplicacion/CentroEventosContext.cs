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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=CentroEventos.sqlite");
            }
        }
    }
}

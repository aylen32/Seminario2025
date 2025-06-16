using System;
using CentroEventos.Aplicacion.Entidades;
using Microsoft.EntityFrameworkCore;
using CentroEventos.Aplicacion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CentroEventos.Repositorios
{
    public class CentroEventosContext : DbContext
    {
        public CentroEventosContext(){}
        
        public CentroEventosContext(DbContextOptions<CentroEventosContext> options) : base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("data source=CentroEventos.sqlite");
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<EventoDeportivo> EventosDeportivos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             // Para que EF Core guarde el DateTime como string en formato ISO 8601
             var dateTimeConverter = new ValueConverter<DateTime, string>(
             v => v.ToString("yyyy/MM/dd"),
             v => DateTime.ParseExact(v, "yyyy/MM/dd", null));

             modelBuilder.Entity<Reserva>()
            .Property(r => r.FechaAltaReserva)
            .HasConversion(dateTimeConverter);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Permisos)
                .HasConversion<int>(); // guarda los flags como int

            base.OnModelCreating(modelBuilder);
        }
    }
}
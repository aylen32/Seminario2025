using System;
using CentroEventos.Aplicacion.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;

namespace CentroEventos.Repositorios
{
    public class CentroEventosContext : DbContext
    {
        public CentroEventosContext(DbContextOptions<CentroEventosContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("data source=CentroEventos.sqlite");
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<EventoDeportivo> EventosDeportivos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // DateTime como string ISO 8601
            var dateTimeConverterEvento = new ValueConverter<DateTime, string>(
            v => v.ToString("yyyy/MM/dd HH:mm"),
            v => DateTime.ParseExact(v, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture)
            );

            modelBuilder.Entity<EventoDeportivo>()
            .Property(e => e.FechaHoraInicio)
            .HasConversion(dateTimeConverterEvento);

            var dateTimeConverterReserva = new ValueConverter<DateTime, string>(
            v => v.ToString("yyyy/MM/dd"),
            v => DateTime.ParseExact(v, "yyyy/MM/dd", CultureInfo.InvariantCulture)
            );

            modelBuilder.Entity<Reserva>()
            .Property(r => r.FechaAltaReserva)
            .HasConversion(dateTimeConverterReserva);

            // Relación muchos-a-muchos con tabla intermedia UsuarioPermiso
            modelBuilder.Entity<UsuarioPermiso>()
                .HasKey(up => new { up.UsuarioId, up.PermisoId });

            modelBuilder.Entity<UsuarioPermiso>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.Permisos)
                .HasForeignKey(up => up.UsuarioId);

            modelBuilder.Entity<UsuarioPermiso>()
                .HasOne(up => up.Permiso)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(up => up.PermisoId);

            // Guardar enum PermisoTipo como string
            modelBuilder.Entity<Permiso>()
                .Property(p => p.Tipo)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }

        // Método para asegurar la base y configurar journal_mode=DELETE
        public void ConfigurarJournalModeDelete()
        {
            // Asegura que la base de datos exista
            Database.EnsureCreated();

            var connection = Database.GetDbConnection();
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA journal_mode=DELETE;";
                command.ExecuteNonQuery();
            }
        }
    }
}
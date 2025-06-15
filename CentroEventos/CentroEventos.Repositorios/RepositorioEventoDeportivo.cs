using System;
using System.Collections.Generic;
using System.Linq;
using CentroEventos.Aplicacion;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;


namespace CentroEventos.Repositorios
{
    public class RepositorioEventoDeportivo : IRepositorioEventoDeportivo
    {
        private readonly CentroEventosContext _context;

        public RepositorioEventoDeportivo(CentroEventosContext context)
        {
            _context = context;
        }

        public bool ExisteEventoPorId(int id)
        {
            return _context.EventosDeportivos.Any(e => e.Id == id);
        }

        public EventoDeportivo? ObtenerEvento(int id)
        {
            return _context.EventosDeportivos.Find(id);
        }

        public IEnumerable<EventoDeportivo> ObtenerTodos()
        {
            return _context.EventosDeportivos.ToList();
        }

        public void AgregarEvento(EventoDeportivo evento)
        {
            if (evento == null)
                throw new ArgumentNullException(nameof(evento));
            
            _context.EventosDeportivos.Add(evento);
            _context.SaveChanges();
        }

        public void ModificarEvento(int id, string nombre, string descripcion, DateTime fecha, double duracion, int cupo)
        {
            var evento = _context.EventosDeportivos.Find(id);
            if (evento == null)
                throw new EntidadNotFoundException($"El evento con ID {id} no existe");

            evento.Nombre = nombre;
            evento.Descripcion = descripcion;
            evento.FechaHoraInicio = fecha;
            evento.DuracionHoras = duracion;
            evento.CupoMaximo = cupo;

            _context.SaveChanges();
        }

        public void EliminarEvento(int id)
        {
            var evento = _context.EventosDeportivos.Find(id);
            if (evento == null)
                throw new EntidadNotFoundException($"El evento con ID {id} no existe");

            _context.EventosDeportivos.Remove(evento);
            _context.SaveChanges();
        }
    }
}
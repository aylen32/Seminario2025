using System.Collections.Generic;
using System.Linq;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Repositorios;
public class RepositorioReserva : IRepositorioReserva
{
    private readonly CentroEventosContext _context;

    public RepositorioReserva(CentroEventosContext context)
    {
        _context = context;
    }

    public void AgregarReserva(Reserva reserva)
    {
        _context.Reservas.Add(reserva);
        _context.SaveChanges();
    }

    public bool EliminarReserva(int id)
    {
        var reserva = _context.Reservas.Find(id);
        if (reserva == null)
            return false;

        _context.Reservas.Remove(reserva);
        _context.SaveChanges();
        return true;
    }

    public bool ModificarReserva(int id, EstadoAsistencia estado)
    {
        var reserva = _context.Reservas.Find(id);
        if (reserva == null)
            return false;

        reserva.Estado = estado;
        _context.SaveChanges();
        return true;
    }

    public Reserva? ObtenerReserva(int id)
    {
        return _context.Reservas.Find(id);
    }

    public List<Reserva> ObtenerReservasPorEvento(int eventoId)
    {
        return _context.Reservas.Where(r => r.EventoDeportivoId == eventoId).ToList();
    }

    public List<Reserva> ObtenerReservasPorPersona(int personaId)
    {
        return _context.Reservas.Where(r => r.PersonaId == personaId).ToList();
    }

    public List<Reserva> ObtenerTodas()
    {
        return _context.Reservas.ToList();
    }

    public bool ExisteReservaPorId(int id)
    {
        return _context.Reservas.Any(r => r.Id == id);
    }

    public bool ExisteReservaDuplicada(int personaId, int eventoId)
    {
        return _context.Reservas.Any(r => r.PersonaId == personaId && r.EventoDeportivoId == eventoId);
    }
}
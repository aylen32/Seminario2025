using System;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
namespace CentroEventos.Aplicacion.Validaciones;

public class ValidadorReserva : IValidadorReserva
{
    private readonly IRepositorioPersona _personaRepo;
    private readonly IRepositorioEventoDeportivo _eventoRepo;
    private readonly IRepositorioReserva _reservaRepo;
    private string? _error;

    public ValidadorReserva(IRepositorioPersona personaRepo,
                             IRepositorioEventoDeportivo eventoRepo,
                             IRepositorioReserva reservaRepo)
    {
        _personaRepo = personaRepo;
        _eventoRepo = eventoRepo;
        _reservaRepo = reservaRepo;
    }

    public bool Validar(Reserva reserva)
    {
        if (!_personaRepo.ExistePersonaPorId(reserva.PersonaId))
        {
            _error = "La persona no existe";
            return false;
        }

        if (!_eventoRepo.ExisteEventoPorId(reserva.EventoDeportivoId))
        {
            _error = "El evento deportivo no existe";
            return false;
        }

        if (_reservaRepo.ExisteReservaDuplicada(reserva.PersonaId, reserva.EventoDeportivoId))
        {
            _error = "Ya existe una reserva para este evento deportivo de esta persona";
            return false;
        }

        var evento = _eventoRepo.ObtenerEvento(reserva.EventoDeportivoId);
        if (evento != null)
        {
            int cantidadReservas = _reservaRepo.ObtenerReservasPorEvento(reserva.EventoDeportivoId).Count();
            if (cantidadReservas >= evento.CupoMaximo)
            {
                _error = "El evento deportivo no tiene cupo disponible";
                return false;
            }
        }

        _error = null;
        return true;
    }

    public string? ObtenerError()
    {
        return _error;
    }
}
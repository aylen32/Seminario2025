using System;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
namespace CentroEventos.Aplicacion.Validaciones;

public class ValidadorReserva : IValidadorReserva
{
    private readonly IRepositorioPersona _personaRepo;
    private readonly IRepositorioEventoDeportivo _eventoRepo;
    private readonly IRepositorioReserva _reservaRepo;

    public ValidadorReserva(IRepositorioPersona personaRepo, IRepositorioEventoDeportivo eventoRepo, IRepositorioReserva reservaRepo)
    {
        _personaRepo = personaRepo;
        _eventoRepo = eventoRepo;
        _reservaRepo = reservaRepo;
    }

    public void Validar(Reserva reserva)
    {

        if (!_personaRepo.ExistePersonaPorId(reserva.PersonaId))
        {
            throw new EntidadNotFoundException("La persona no existe");
        }

        if (!_eventoRepo.ExisteEventoPorId(reserva.EventoDeportivoId))
        {
            throw new EntidadNotFoundException("El evento deportivo no existe");
        }
        if (_reservaRepo.ExisteReservaDuplicada(reserva.PersonaId, reserva.EventoDeportivoId))
        {
            throw new DuplicadoException("Ya existe una reserva para este evento deportivo de esta persona");
        }
        if (_eventoRepo.ObtenerEvento(reserva.EventoDeportivoId).CupoMaximo <= _reservaRepo.ObtenerReservasPorEvento(reserva.EventoDeportivoId).Count())
        {
            throw new CupoExcedidoException("El evento deportivo no tiene cupo disponible");
        }
    }
}
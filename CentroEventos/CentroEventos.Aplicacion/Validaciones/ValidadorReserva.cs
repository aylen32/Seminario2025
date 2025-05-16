using System;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
namespace CentroEventos.Aplicacion.Validaciones;               //Falta reglas del negocio

public class ValidadorReserva: IValidadorReserva
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

        EventoDeportivo ? evento = _eventoRepo.ObtenerEvento(reserva.EventoDeportivoId);
        if (evento != null) //pobre manejo de null, pero manejo al fin
        {
            if (evento.CupoMaximo <= _reservaRepo.ObtenerReservasPorEvento(reserva.EventoDeportivoId).Count())
        {
            throw new CupoExcedidoException("El evento deportivo no tiene cupo disponible");
        }
        }
        
    }

    //Falta la validacion de que una persona no reserve dos veces el mismo evento deportivo
    //Falta la validacion de que haya cupo disponible en el EventoDeportivo antes de guardar
}
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

    public void Validar(Reserva reserva){

        if (!_personaRepo.ExistePersonaPorId(reserva.PersonaId)){
            throw new EntidadNotFoundException ("La persona no existe");
        }

        if (!_eventoRepo.ExisteEventoPorId(reserva.EventoDeportivoId)){
            throw new EntidadNotFoundException ("El evento deportivo no existe");
        }
    }

    //Falta la validacion de que una persona no reserve dos veces el mismo evento deportivo
    //Falta la validacion de que haya cupo disponible en el EventoDeportivo antes de guardar
}
using System;

namespace CentroEventos.Aplicacion.CasosUso;

public class AltaReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IValidadorReserva _validadorReserva;

    public AltaReservaUseCase (IRepositorioReserva repositorioReserva, IValidadorReserva validadorReserva){

        _repositorioReserva = repositorioReserva;        //Inyeccion de dependencia por constructor (supuestamente se puede)
        _validadorReserva = validadorReserva;            //No se si esto estaria bien! ¡Consultar!
    }

    public void Ejecutar (Reserva reserva){

        _validadorReserva.Validar(reserva);             // Validar la reserva antes de guardarla
        _repositorioReserva.AgregarReserva(reserva);    // Agregar la reserva al repositorio
    }
}

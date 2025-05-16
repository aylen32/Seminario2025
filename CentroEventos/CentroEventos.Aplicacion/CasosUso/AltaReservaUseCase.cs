using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosUso;

public class AltaReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IValidadorReserva _validadorReserva;
    private readonly IServicioAutorizacion _servicioAutorizacion;

    public AltaReservaUseCase(IRepositorioReserva repositorioReserva, IValidadorReserva validadorReserva, IServicioAutorizacion servicioAutorizacion, IRepositorioEventoDeportivo repositorioEventoDeportivo, IRepositorioPersona repositorioPersona)
    {

        _repositorioEventoDeportivo = repositorioEventoDeportivo; // Inyeccion de dependencia por constructor (supuestamente se puede)
        _repositorioPersona = repositorioPersona;               // Inyeccion de dependencia por constructor (supuestamente se puede)
        _repositorioReserva = repositorioReserva;        //Inyeccion de dependencia por constructor (supuestamente se puede)
        _validadorReserva = validadorReserva;  
        _servicioAutorizacion = servicioAutorizacion;    //No se si esto estaria bien! ¡Consultar!
    }

    public void Ejecutar (Reserva reserva, int idUsuario){

        // Verificar si el usuario tiene permiso para reservar
        if (!_servicioAutorizacion.PoseeElPermiso(idUsuario, Permisos.EventoAlta))
        {
            throw new FalloAutorizacionException("El usuario no tiene permiso para realizar reservas.");
        }
        _validadorReserva.Validar(reserva);
        _repositorioReserva.AgregarReserva(reserva);    // Agregar la reserva al repositorio
    }
}

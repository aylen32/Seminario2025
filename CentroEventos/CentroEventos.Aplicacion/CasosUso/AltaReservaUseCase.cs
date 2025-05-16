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

        _repositorioEventoDeportivo = repositorioEventoDeportivo;  
        _repositorioPersona = repositorioPersona;              
        _repositorioReserva = repositorioReserva;        
        _validadorReserva = validadorReserva;  
        _servicioAutorizacion = servicioAutorizacion;    
    }

    public void Ejecutar (Reserva reserva, int idUsuario){

        // Verificar si el usuario tiene permiso para reservar
        if (!_servicioAutorizacion.PoseeElPermiso(idUsuario, Permisos.EventoAlta))
        {
            throw new FalloAutorizacionException("El usuario no tiene permiso para realizar reservas");
        }
        _validadorReserva.Validar(reserva);
        _repositorioReserva.AgregarReserva(reserva);    
    }
}

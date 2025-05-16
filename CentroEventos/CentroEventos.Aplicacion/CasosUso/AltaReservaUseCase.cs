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
    private readonly IServicioAutorizacion _autorizacion;
    private readonly int _idUsuario;

    public AltaReservaUseCase(IRepositorioReserva repositorioReserva, IValidadorReserva validadorReserva, IRepositorioEventoDeportivo repositorioEventoDeportivo, IRepositorioPersona repositorioPersona, IServicioAutorizacion autorizacion,
    int idUsuario)
    {

        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _repositorioPersona = repositorioPersona;
        _repositorioReserva = repositorioReserva;
        _validadorReserva = validadorReserva;
        _autorizacion = autorizacion;
        _idUsuario = idUsuario;
    }

    public void Ejecutar (Reserva reserva, int idUsuario){

        // Verificar si el usuario tiene permiso para reservar
        if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.EventoAlta))
        {
            throw new FalloAutorizacionException("El usuario no tiene permiso para realizar reservas");
        }
        _validadorReserva.Validar(reserva);
        _repositorioReserva.AgregarReserva(reserva);    
    }
}

using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.CasosUso;
public class AltaReservaUseCase
{
    private readonly IRepositorioReserva _reservaRepo;
    private readonly IValidadorReserva _validadorReserva;
    private readonly IServicioAutorizacion _autorizacion;

    public AltaReservaUseCase(
        IRepositorioReserva reservaRepo,
        IRepositorioPersona personaRepo,
        IRepositorioEventoDeportivo eventoRepo,
        IValidadorReserva validadorReserva,
        IServicioAutorizacion autorizacion)
    {
        _reservaRepo = reservaRepo;
        _validadorReserva = validadorReserva;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Reserva reserva, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, PermisoTipo.ReservaAlta))
            throw new OperacionInvalidaException("No tiene permiso para dar de alta reservas");

        if (!_validadorReserva.ValidarParaAlta(reserva))
            throw new ValidacionException(_validadorReserva.ObtenerError()!);

        _reservaRepo.AgregarReserva(reserva);
    }
}
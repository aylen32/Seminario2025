using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosUso;
public class AltaReservaUseCase
{
    private readonly IRepositorioReserva _reservaRepo;
    private readonly IRepositorioPersona _personaRepo;
    private readonly IRepositorioEventoDeportivo _eventoRepo;
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
        _personaRepo = personaRepo;
        _eventoRepo = eventoRepo;
        _validadorReserva = validadorReserva;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Reserva reserva, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.ReservaAlta))
            throw new FalloAutorizacionException("No tiene permiso para dar de alta reservas");

        if (!_validadorReserva.Validar(reserva))
            throw new ValidacionException(_validadorReserva.ObtenerError()!);

        _reservaRepo.AgregarReserva(reserva);
    }
}

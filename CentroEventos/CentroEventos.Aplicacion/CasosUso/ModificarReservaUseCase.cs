using System;

namespace CentroEventos.Aplicacion.CasosUso;

using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;

public class ModificarReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IValidadorReserva _validadorReserva;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly int _idUsuario;

    public ModificarReservaUseCase(IRepositorioReserva repo, IValidadorReserva validador, IServicioAutorizacion autorizacion,
    int idUsuario)
    {
        _repositorioReserva = repo;
        _validadorReserva = validador;
        _autorizacion = autorizacion;
        _idUsuario = idUsuario;
    }

    public void Ejecutar(Reserva reserva)
    {
        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.ReservaModificacion))
            throw new FalloAutorizacionException("No tiene permiso para modificar reservas");

        if (!_repositorioReserva.ExisteReservaPorId(reserva.Id))
            throw new EntidadNotFoundException($"La reserva con ID {reserva.Id} no existe");

        if (!_validadorReserva.Validar(reserva))
            throw new ValidacionException(_validadorReserva.ObtenerError()!);

        _repositorioReserva.ModificarReserva(reserva.Id, reserva.Estado);
    }

}
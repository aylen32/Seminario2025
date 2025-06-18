using System;

namespace CentroEventos.Aplicacion.CasosUso;

using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Entidades;

public class ModificarReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IValidadorReserva _validadorReserva;
    private readonly IServicioAutorizacion _autorizacion;

    public ModificarReservaUseCase(IRepositorioReserva repo, IValidadorReserva validador, IServicioAutorizacion autorizacion)
    {
        _repositorioReserva = repo;
        _validadorReserva = validador;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Reserva reserva, int idUsuario)
    {
      // if (!_autorizacion.PoseeElPermiso(idUsuario, PermisoTipo.ReservaModificacion))
      //     throw new FalloAutorizacionException("No tiene permiso para modificar reservas");

      var original = _repositorioReserva.ObtenerReserva(reserva.Id);

      if (original == null)
        throw new EntidadNotFoundException($"La reserva con ID {reserva.Id} no existe");

      original.Estado = reserva.Estado;

      if (!_validadorReserva.ValidarParaModificacion(original))
        throw new ValidacionException(_validadorReserva.ObtenerError()!);

      _repositorioReserva.ModificarReserva(original.Id, original.Estado);
    }
}

using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;
namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Excepciones;

public class BajaReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _autorizacion;

    public BajaReservaUseCase(IRepositorioReserva repositorioReserva, IServicioAutorizacion autorizacion)
    {
        _repositorioReserva = repositorioReserva;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(int idReserva, int idUsuario)
    {
    //    if (!_autorizacion.PoseeElPermiso(idUsuario, PermisoTipo.ReservaBaja))
      //      throw new FalloAutorizacionException("No tiene permiso para eliminar reservas");

        if (!_repositorioReserva.ExisteReservaPorId(idReserva))
            throw new EntidadNotFoundException($"La reserva con ID {idReserva} no existe");

        _repositorioReserva.EliminarReserva(idReserva);
    }
}


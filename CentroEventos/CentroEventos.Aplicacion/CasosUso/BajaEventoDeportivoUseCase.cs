using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
namespace CentroEventos.Aplicacion.CasosUso;

using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;

public class BajaEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _autorizacion;

    public BajaEventoDeportivoUseCase(
        IRepositorioEventoDeportivo repositorioEventoDeportivo, 
        IRepositorioReserva repositorioReserva, 
        IServicioAutorizacion autorizacion)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _repositorioReserva = repositorioReserva;
        _autorizacion = autorizacion;
    }
  
    public void Ejecutar(int idEvento, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.EventoBaja))
            throw new FalloAutorizacionException("No tiene permiso para eliminar eventos");

        if (!_repositorioEventoDeportivo.ExisteEventoPorId(idEvento))
            throw new EntidadNotFoundException($"El evento con ID {idEvento} no existe");

        var reservas = _repositorioReserva.ObtenerReservasPorEvento(idEvento);
        if (reservas.Any())
            throw new OperacionInvalidaException("No se puede eliminar el evento porque tiene reservas asociadas");

        _repositorioEventoDeportivo.EliminarEvento(idEvento);
    }
}
    
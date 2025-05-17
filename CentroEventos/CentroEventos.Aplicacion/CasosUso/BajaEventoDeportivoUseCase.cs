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
    private readonly int _idUsuario;

    public BajaEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo, IRepositorioReserva repositorioReserva, IServicioAutorizacion autorizacion,
    int idUsuario)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _repositorioReserva = repositorioReserva;
        _autorizacion = autorizacion;
        _idUsuario = idUsuario;
    }
  
    public void Ejecutar(int id)
    {
        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.EventoBaja))
        {
            throw new FalloAutorizacionException("No tiene permiso para eliminar eventos");
        }
        if (!_repositorioEventoDeportivo.ExisteEventoPorId(id))
            {
                throw new EntidadNotFoundException($"El evento con ID {id} no existe");
            }
        IEnumerable<Reserva> reservas = _repositorioReserva.ObtenerReservasPorEvento(id);
        if (reservas.Any()) // Verificar si el evento tiene reservas
        {
            throw new OperacionInvalidaException("No se puede eliminar el evento porque tiene reservas asociadas");
        }
        _repositorioEventoDeportivo.EliminarEvento(id);
    }
}    
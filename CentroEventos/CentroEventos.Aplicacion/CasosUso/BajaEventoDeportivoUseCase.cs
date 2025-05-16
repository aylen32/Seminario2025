using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
namespace CentroEventos.Aplicacion.CasosUso;

public class BajaEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IRepositorioReserva _repositorioReserva;
   
    public BajaEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo, IRepositorioReserva repositorioReserva)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _repositorioReserva = repositorioReserva;
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
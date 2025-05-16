using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
namespace CentroEventos.Aplicacion.CasosUso;

public class ListadoReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    public ListadoReservaUseCase(IRepositorioReserva repositorioReserva)
    {
        _repositorioReserva = repositorioReserva; 
    }

    public IEnumerable<Reserva> ? Ejecutar()
    {
        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.ReservaAlta))
        {
            throw new FalloAutorizacionException("No tiene permiso para listar reservas");
        }

        return _repositorioReserva.ObtenerTodas();
    }
    
}

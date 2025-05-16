using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
namespace CentroEventos.Aplicacion.CasosUso;

public class BajaReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
   
    public BajaReservaUseCase(IRepositorioReserva repositorioReserva)
    {
        _repositorioReserva = repositorioReserva;
      
    }
    public void Ejecutar(int id)
    {
        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.ReservaBaja))
        {
            throw new FalloAutorizacionException("No tiene permiso para eliminar reservas");
        }
        if (!_repositorioReserva.ExisteReservaPorId(id))
            {
                throw new EntidadNotFoundException($"La reserva con ID {id} no existe");
            }
        _repositorioReserva.EliminarReserva(id);
    }

}

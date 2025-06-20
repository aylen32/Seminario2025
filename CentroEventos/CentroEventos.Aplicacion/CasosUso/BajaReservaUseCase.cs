using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.CasosUso;

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
        if (!_autorizacion.TienePermisoBaja(idUsuario))
            throw new OperacionInvalidaException("No tiene permiso para eliminar reservas");

        if (!_repositorioReserva.ExisteReservaPorId(idReserva))
            throw new EntidadNotFoundException($"La reserva con ID {idReserva} no existe");

        bool eliminado = _repositorioReserva.EliminarReserva(idReserva);
        if (!eliminado)
            throw new EntidadNotFoundException($"La reserva con ID {idReserva} no existe");
    }
}
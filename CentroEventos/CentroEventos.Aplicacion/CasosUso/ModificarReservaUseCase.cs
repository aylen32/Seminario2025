using System;

namespace CentroEventos.Aplicacion.CasosUso;

using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;

public class ModificarReservaUseCase
{
    private readonly IRepositorioReserva _repo;
    private readonly IValidadorReserva _validador;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly int _idUsuario;

    public ModificarReservaUseCase(IRepositorioReserva repo, IValidadorReserva validador, IServicioAutorizacion autorizacion,
    int idUsuario)
    {
        _repo = repo;
        _validador = validador;
        _autorizacion = autorizacion;
        _idUsuario = idUsuario;
    }

    public void Ejecutar(int id, Reserva.EstadoAsistencia estado)
    {
        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.ReservaModificacion))
        {
            throw new FalloAutorizacionException("No tiene permiso para modificar reservas");
        }
        if (!_repo.ExisteReservaPorId(id))
            {
                throw new EntidadNotFoundException($"La reserva con ID {id} no existe");
            }
        //_validador.Validar(reserva);
        _repo.ModificarReserva(id, estado);
    }
}
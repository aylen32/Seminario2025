using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Servicio;

namespace CentroEventos.Aplicacion.CasosUso;

public class ObtenerReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _autorizacion;

    public ObtenerReservaUseCase(IRepositorioReserva repo, IServicioAutorizacion autorizacion)
    {
        _repositorioReserva = repo;
        _autorizacion = autorizacion;
    }

    public Reserva? Ejecutar(int Idreserva, int idUsuario)
    {
        return _repositorioReserva.ObtenerReserva(Idreserva);
    }
}
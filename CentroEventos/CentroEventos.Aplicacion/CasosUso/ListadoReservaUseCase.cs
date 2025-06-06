using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;
namespace CentroEventos.Aplicacion.CasosUso;

public class ListadoReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly int _idUsuario;
    public ListadoReservaUseCase(IRepositorioReserva repositorioReserva, IServicioAutorizacion autorizacion,
    int idUsuario)
    {
        _repositorioReserva = repositorioReserva;
        _autorizacion = autorizacion;
        _idUsuario = idUsuario;
    }

    public IEnumerable<Reserva> ? Ejecutar()
    {
        return _repositorioReserva.ObtenerTodas();
    }
    
}

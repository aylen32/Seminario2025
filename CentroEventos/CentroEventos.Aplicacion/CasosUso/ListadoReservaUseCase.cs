using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosUso;

public class ListadoReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _autorizacion;
    public ListadoReservaUseCase(IRepositorioReserva repositorioReserva, IServicioAutorizacion autorizacion)
    {
        _repositorioReserva = repositorioReserva;
        _autorizacion = autorizacion;
    }

    public IEnumerable<Reserva> ? Ejecutar()
    {
        return _repositorioReserva.ObtenerTodas();
    }
    
}

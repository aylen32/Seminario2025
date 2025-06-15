using System;

namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;

public class ListarEventosConCupoDisponibleUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _autorizacion;

    public ListarEventosConCupoDisponibleUseCase(IRepositorioEventoDeportivo repositorioEvento, IRepositorioReserva repositorioReserva, IServicioAutorizacion autorizacion)
    {
        _repositorioEvento = repositorioEvento;
        _repositorioReserva = repositorioReserva;
        _autorizacion = autorizacion;
    }

    public List<EventoDeportivo> ? Ejecutar()
    {
        List<EventoDeportivo> eventosConCupo = new List<EventoDeportivo>();
        foreach (EventoDeportivo ev in _repositorioEvento.ObtenerTodos())
        {
            if (ev.FechaHoraInicio > DateTime.Now)
            {
                int cantidadReservas = _repositorioReserva.ObtenerReservasPorEvento(ev.Id).Count();
                if (ev.CupoMaximo > cantidadReservas)
                {
                    eventosConCupo.Add(ev);
                }
            }
        }
        return eventosConCupo;
    }  
}
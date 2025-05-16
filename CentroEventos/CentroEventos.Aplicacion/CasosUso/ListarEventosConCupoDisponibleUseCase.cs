using System;

namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;

public class ListarEventosConCupoDisponibleUseCase
{


    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IRepositorioReserva _repositorioReserva;
    public ListarEventosConCupoDisponibleUseCase(IRepositorioEventoDeportivo repositorioEvento, IRepositorioReserva repositorioReserva)
    {
        _repositorioEvento = repositorioEvento;
        _repositorioReserva = repositorioReserva;
    }

    public List<EventoDeportivo> ? ejecutar()

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

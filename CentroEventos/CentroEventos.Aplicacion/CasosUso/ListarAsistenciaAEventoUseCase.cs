using System;

namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;

public class ListarAsistenciaAEventoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IRepositorioReserva _repositorioReserva;

    public ListarAsistenciaAEventoUseCase(IRepositorioEventoDeportivo repositorioEvento, IRepositorioPersona repositorioPersona, IRepositorioReserva repositorioReserva)
    {
        _repositorioEvento = repositorioEvento; // Inyeccion de dependencia por constructor (supuestamente se puede)
        _repositorioPersona = repositorioPersona; // Inyeccion de dependencia por constructor (supuestamente se puede)
        _repositorioReserva = repositorioReserva; // Inyeccion de dependencia por constructor (supuestamente se puede)
    }
   


    public IEnumerable<Persona>? Ejecutar(int id)
    {
        var personas = new List<Persona>();
        foreach (Reserva reserva in _repositorioReserva.ObtenerReservasPorEvento(id))
        {
            if (reserva.Estado == Reserva.EstadoAsistencia.Presente)
            {
                Persona ? persona = _repositorioPersona.ObtenerPersona(reserva.PersonaId);
                if (persona != null)
                {
                    personas.Add(persona);
                }
            }

        }
        return personas;
    }


}

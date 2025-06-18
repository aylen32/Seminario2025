using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.CasosUso;

public class ListarAsistenciaAEventoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IRepositorioReserva _repositorioReserva;

    public ListarAsistenciaAEventoUseCase(IRepositorioEventoDeportivo repositorioEvento, IRepositorioPersona repositorioPersona, IRepositorioReserva repositorioReserva)
    {
        _repositorioEvento = repositorioEvento;
        _repositorioPersona = repositorioPersona;
        _repositorioReserva = repositorioReserva;
    }

    public List<Persona> Ejecutar(int id)
    {
        if (!_repositorioEvento.ExisteEventoPorId(id))
            {
                throw new EntidadNotFoundException($"Evento con ID {id} no encontrado");
            }
        var personas = new List<Persona>();
        foreach (Reserva reserva in _repositorioReserva.ObtenerReservasPorEvento(id))
        {
            if (reserva.Estado == EstadoAsistencia.Presente)
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
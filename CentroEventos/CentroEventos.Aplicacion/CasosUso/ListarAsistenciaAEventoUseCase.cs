using System;

namespace CentroEventos.Aplicacion.CasosUso;

using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Enumerativos;


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



    public IEnumerable<Persona>? Ejecutar(int id)
    {
        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.EventoAlta))
        {
            throw new FalloAutorizacionException("No tiene permiso para consultar la asistencia a eventos");
        }
        if (!_repositorioEvento.ExisteEventoPorId(id))
            {
                throw new EntidadNotFoundException($"Evento con ID {id} no encontrado.");
            }
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

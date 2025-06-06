using System;

namespace CentroEventos.Aplicacion.CasosUso;

using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;


public class ListarAsistenciaAEventoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly int _idUsuario;

    public ListarAsistenciaAEventoUseCase(IRepositorioEventoDeportivo repositorioEvento, IRepositorioPersona repositorioPersona, IRepositorioReserva repositorioReserva, IServicioAutorizacion autorizacion,
    int idUsuario)
    {
        _repositorioEvento = repositorioEvento;
        _repositorioPersona = repositorioPersona;
        _repositorioReserva = repositorioReserva;
        _autorizacion = autorizacion;
        _idUsuario = idUsuario;
    }



    public IEnumerable<Persona>? Ejecutar(int id)
    {
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
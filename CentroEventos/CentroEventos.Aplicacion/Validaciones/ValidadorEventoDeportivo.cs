using System;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
namespace CentroEventos.Aplicacion.Validaciones;         

public class ValidadorEventoDeportivo : IValidadorEventoDeportivo
{
    private readonly IRepositorioPersona _personaRepo;
    private string? _error;

    public ValidadorEventoDeportivo(IRepositorioPersona personaRepo)
    {
        _personaRepo = personaRepo;
    }

    public bool Validar(EventoDeportivo eventoDeportivo)
    {
        if (string.IsNullOrWhiteSpace(eventoDeportivo.Nombre))
        {
            _error = "El Nombre del evento no puede estar vacío";
            return false;
        }

        if (string.IsNullOrWhiteSpace(eventoDeportivo.Descripcion))
        {
            _error = "La Descripción del evento no puede estar vacía";
            return false;
        }

        if (eventoDeportivo.FechaHoraInicio < DateTime.Now)
        {
            _error = "La fecha y hora de inicio del evento ingresada ya pasó";
            return false;
        }

        if (eventoDeportivo.CupoMaximo <= 0)
        {
            _error = "El Cupo debe ser mayor que cero";
            return false;
        }

        if (eventoDeportivo.DuracionHoras <= 0)
        {
            _error = "La duración del evento debe ser mayor que cero";
            return false;
        }

        if (!_personaRepo.ExistePersonaPorId(eventoDeportivo.ResponsableId))
        {
            _error = "El Responsable del evento no existe";
            return false;
        }

        _error = null;
        return true;
    }

    public string? ObtenerError()
    {
        return _error;
    }
}


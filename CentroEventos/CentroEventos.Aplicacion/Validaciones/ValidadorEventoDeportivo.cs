using System;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
namespace CentroEventos.Aplicacion.Validaciones;         

public class ValidadorEventoDeportivo: IValidadorEventoDeportivo
{
    private readonly IRepositorioPersona _personaRepo;

    public ValidadorEventoDeportivo(IRepositorioPersona personaRepo)
    {
        _personaRepo = personaRepo;
    }

    public void Validar(EventoDeportivo eventoDeportivo){

        if (string.IsNullOrWhiteSpace (eventoDeportivo.Nombre)){
            throw new ValidacionException ("El Nombre del evento no puede estar vacío");
        }

        if (string.IsNullOrWhiteSpace (eventoDeportivo.Descripcion)){
            throw new ValidacionException ("La Descripción del evento no puede estar vacía");
        }

        if (eventoDeportivo.FechaHoraInicio < DateTime.Now){
            throw new ValidacionException ("La fecha y hora de inicio del evento ingresada ya paso");
        }

        if (eventoDeportivo.CupoMaximo <= 0){
            throw new ValidacionException ("El Cupo debe ser mayor que cero");
        }

        if (eventoDeportivo.DuracionHoras <= 0){
            throw new ValidacionException ("La duración del evento debe ser mayor que cero");
        }

        if (!_personaRepo.ExistePersonaPorId(eventoDeportivo.ResponsableId)){
            throw new EntidadNotFoundException ("El Responsable del evento no existe");
        }
    }
}

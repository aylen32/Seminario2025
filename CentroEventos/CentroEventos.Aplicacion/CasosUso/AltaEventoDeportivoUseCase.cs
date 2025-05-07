using System;

namespace CentroEventos.Aplicacion.CasosUso;

public class AltaEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IValidadorEventoDeportivo _validadorEventoDeportivo;

    public AltaEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo, IValidadorEventoDeportivo validadorEventoDeportivo){

        _repositorioEventoDeportivo = repositorioEventoDeportivo;       //Inyeccion de dependencia por constructor (supuestamente se puede)
        _validadorEventoDeportivo = validadorEventoDeportivo;           //No se si esto estaria bien! ¡Consultar!
    }
    public void Ejecutar(EventoDeportivo eventoDeportivo){

      _validadorEventoDeportivo.Validar(eventoDeportivo);                    // Validar el evento antes de guardarlo
      _repositorioEventoDeportivo.AgregarEventoDeportivo(eventoDeportivo);   // Agregar el evento deportivo
    }
}

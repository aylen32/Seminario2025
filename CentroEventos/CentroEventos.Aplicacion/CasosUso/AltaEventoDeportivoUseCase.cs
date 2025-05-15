using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
namespace CentroEventos.Aplicacion.CasosUso;

public class AltaEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IValidadorEventoDeportivo _validadorEventoDeportivo; //falta crear Interfaz??
    public AltaEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo, IValidadorEventoDeportivo validadorEventoDeportivo){

        _repositorioEventoDeportivo = repositorioEventoDeportivo;       //Inyeccion de dependencia por constructor (supuestamente se puede)
        _validadorEventoDeportivo = validadorEventoDeportivo;           //No se si esto estaria bien! ¡Consultar!
    }
    public void Ejecutar(EventoDeportivo eventoDeportivo){

      _validadorEventoDeportivo.Validar(eventoDeportivo);                    // Validar el evento antes de guardarlo
      _repositorioEventoDeportivo.AgregarEvento(eventoDeportivo);   // Agregar el evento deportivo
    }
}

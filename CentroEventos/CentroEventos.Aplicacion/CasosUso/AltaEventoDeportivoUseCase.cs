using System;

using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;

public class AltaEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IValidadorEventoDeportivo _validadorEventoDeportivo;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly int _idUsuario;

  public AltaEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo, IValidadorEventoDeportivo validadorEventoDeportivo, IServicioAutorizacion autorizacion,
  int idUsuario)
  {
    _repositorioEventoDeportivo = repositorioEventoDeportivo;       //Inyeccion de dependencia por constructor (supuestamente se puede)
    _validadorEventoDeportivo = validadorEventoDeportivo;
    _autorizacion = autorizacion;
    _idUsuario = idUsuario;
  }
    public void Ejecutar(EventoDeportivo eventoDeportivo){

    if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.EventoAlta))
    {
      throw new FalloAutorizacionException("No tiene permiso para dar de alta eventos");
    }

      _validadorEventoDeportivo.Validar(eventoDeportivo);                    // Validar el evento antes de guardarlo
      _repositorioEventoDeportivo.AgregarEvento(eventoDeportivo);            // Agregar el evento deportivo
    }
}

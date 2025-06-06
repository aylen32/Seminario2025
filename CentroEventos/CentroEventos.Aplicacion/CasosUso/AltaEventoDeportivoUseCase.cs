using System;

using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;

public class AltaEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IValidadorEventoDeportivo _validadorEvento;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly int _idUsuario;

    public AltaEventoDeportivoUseCase(
        IRepositorioEventoDeportivo repositorioEvento,
        IValidadorEventoDeportivo validadorEvento,
        IServicioAutorizacion autorizacion,
        int idUsuario)
    {
        _repositorioEvento = repositorioEvento;
        _validadorEvento = validadorEvento;
        _autorizacion = autorizacion;
        _idUsuario = idUsuario;
    }

    public void Ejecutar(EventoDeportivo evento)
    {
        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.EventoAlta))
            throw new FalloAutorizacionException("No tiene permiso para dar de alta eventos deportivos");

        if (!_validadorEvento.Validar(evento))
            throw new ValidacionException(_validadorEvento.ObtenerError() ?? "Error desconocido en la validación del evento");

        _repositorioEvento.AgregarEvento(evento);
    }
}

  
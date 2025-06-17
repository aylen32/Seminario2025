using System;

using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Entidades;

public class AltaEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IValidadorEventoDeportivo _validadorEvento;
    private readonly IServicioAutorizacion _autorizacion;

    public AltaEventoDeportivoUseCase(
        IRepositorioEventoDeportivo repositorioEvento,
        IValidadorEventoDeportivo validadorEvento,
        IServicioAutorizacion autorizacion)
    {
        _repositorioEvento = repositorioEvento;
        _validadorEvento = validadorEvento;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(EventoDeportivo evento, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, PermisoTipo.EventoAlta))
            throw new FalloAutorizacionException("No tiene permiso para dar de alta eventos deportivos");

        if (!_validadorEvento.Validar(evento))
            throw new ValidacionException(_validadorEvento.ObtenerError() ?? "Error desconocido en la validación del evento");

        _repositorioEvento.AgregarEvento(evento);
    }
} 
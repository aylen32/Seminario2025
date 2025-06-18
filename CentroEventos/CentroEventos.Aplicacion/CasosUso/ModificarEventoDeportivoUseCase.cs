using System;

namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Entidades;

public class ModificarEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IValidadorEventoDeportivo _validadorEvento;
    private readonly IServicioAutorizacion _autorizacion;

    public ModificarEventoDeportivoUseCase(
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
     //   if (!_autorizacion.PoseeElPermiso(idUsuario, PermisoTipo.EventoModificacion))
       //     throw new FalloAutorizacionException("No tiene permiso para modificar eventos deportivos");

        if (!_repositorioEvento.ExisteEventoPorId(evento.Id))
            throw new EntidadNotFoundException($"El evento con ID {evento.Id} no existe");

        if (!_validadorEvento.Validar(evento))
            throw new ValidacionException(_validadorEvento.ObtenerError() ?? "Error desconocido en la validación del evento");

        _repositorioEvento.ModificarEvento(evento.Id, evento.Nombre, evento.Descripcion, evento.FechaHoraInicio, evento.DuracionHoras, evento.CupoMaximo);
    }
}

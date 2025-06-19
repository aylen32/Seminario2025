using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosUso;
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
        if (!_autorizacion.PoseeElPermiso(idUsuario, PermisoTipo.EventoModificacion))
            throw new OperacionInvalidaException("No tiene permiso para modificar eventos deportivos");

        var eventoOriginal = _repositorioEvento.ObtenerEvento(evento.Id);
        if (eventoOriginal == null)
            throw new EntidadNotFoundException($"El evento con ID {evento.Id} no existe");

        if (eventoOriginal.FechaHoraInicio < DateTime.Now)
            throw new OperacionInvalidaException("No se puede modificar un evento deportivo que ya ocurrió");

        if (!_validadorEvento.Validar(evento))
            throw new ValidacionException(_validadorEvento.ObtenerError() ?? "Error desconocido en la validación del evento");

        _repositorioEvento.ModificarEvento(evento.Id, evento.Nombre, evento.Descripcion, evento.FechaHoraInicio, evento.DuracionHoras, evento.CupoMaximo);
    }
}
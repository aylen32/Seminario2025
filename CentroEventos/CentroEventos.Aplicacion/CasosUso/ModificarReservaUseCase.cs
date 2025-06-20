using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosUso;

public class ModificarReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IValidadorReserva _validadorReserva;
    private readonly IServicioAutorizacion _autorizacion;

    public ModificarReservaUseCase(IRepositorioReserva repo, IValidadorReserva validador, IServicioAutorizacion autorizacion)
    {
        _repositorioReserva = repo;
        _validadorReserva = validador;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Reserva reserva, int idUsuario)
    {
      if (!_autorizacion.TienePermiso(idUsuario, PermisoTipo.ReservaModificacion))
        throw new FalloAutorizacionException("No tenés permiso para modificar reservas.");

      var original = _repositorioReserva.ObtenerReserva(reserva.Id);

      if (original == null)
        throw new EntidadNotFoundException($"La reserva con ID {reserva.Id} no existe.");

      // Validar que el nuevo estado sea válido
      if (!Enum.IsDefined(typeof(EstadoAsistencia), reserva.Estado))
        throw new OperacionInvalidaException($"El estado '{reserva.Estado}' no es válido.");

      var nueva = Reserva.CrearNueva();
      nueva.Id = original.Id;
      nueva.PersonaId = original.PersonaId;
      nueva.EventoDeportivoId = original.EventoDeportivoId;
      nueva.Estado = reserva.Estado;

      if (!_validadorReserva.ValidarParaModificacion(nueva))
        throw new ValidacionException(_validadorReserva.ObtenerError()!);

      // Aplicar el cambio si pasó la validación
      bool modificado = _repositorioReserva.ModificarReserva(original.Id, reserva.Estado);
      if (!modificado)
        throw new OperacionInvalidaException("No se pudo modificar la reserva, inténtelo nuevamente.");
    }
}
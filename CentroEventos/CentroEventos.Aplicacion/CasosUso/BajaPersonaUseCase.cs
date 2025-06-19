using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.CasosUso;
public class BajaPersonaUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;

    public BajaPersonaUseCase(
        IRepositorioPersona repositorioPersona, 
        IRepositorioReserva repositorioReserva, 
        IRepositorioEventoDeportivo repositorioEventoDeportivo)
    {
        _repositorioPersona = repositorioPersona;
        _repositorioReserva = repositorioReserva;
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
    }

    public void Ejecutar(int idPersona)
    {
        // Validar existencia
        if (!_repositorioPersona.ExistePersonaPorId(idPersona))
        {
            throw new EntidadNotFoundException($"La persona con ID {idPersona} no existe.");
        }

        // Validar que no sea responsable de eventos
        var esResponsable = _repositorioEventoDeportivo
            .ObtenerTodos()
            .Any(ev => ev.ResponsableId == idPersona);

        if (esResponsable)
        {
            throw new OperacionInvalidaException("No se puede eliminar la persona porque es responsable de al menos un evento.");
        }

        // Validar que no tenga reservas asociadas
        var tieneReservas = _repositorioReserva
            .ObtenerReservasPorPersona(idPersona)
            .Any();

        if (tieneReservas)
        {
            throw new OperacionInvalidaException("No se puede eliminar la persona porque tiene reservas asociadas.");
        }

        _repositorioPersona.EliminarPersona(idPersona);
    }
}
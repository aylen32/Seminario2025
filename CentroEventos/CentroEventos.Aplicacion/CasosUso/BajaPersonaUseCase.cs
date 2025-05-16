using System;

namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;



public class BajaPersonaUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;

    public BajaPersonaUseCase(IRepositorioPersona repositorioPersona, IRepositorioReserva repositorioReserva, IRepositorioEventoDeportivo repositorioEventoDeportivo)
    {
        _repositorioReserva = repositorioReserva;       
        _repositorioPersona = repositorioPersona;     
        _repositorioEventoDeportivo = repositorioEventoDeportivo;      
    }

    public void Ejecutar(int id){
        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.UsuarioBaja))
        {
            throw new FalloAutorizacionException("No tiene permiso para eliminar personas");
        }
        if (!_repositorioPersona.ExistePersonaPorId(id))
            {
                throw new EntidadNotFoundException($"La persona con ID {id} no existe");
            }
        IEnumerable<EventoDeportivo> eventos = _repositorioEventoDeportivo.ObtenerTodos();
        foreach (var evento in eventos)
        {
            if (evento.ResponsableId == id) // Verificar si la persona tiene eventos a su nombre
            {
                throw new OperacionInvalidaException("No se puede eliminar la persona porque es responsable de al menos un evento");
            }
        }
        var persona = _repositorioPersona.ObtenerPersona(id);
        IEnumerable<Reserva> reservas = _repositorioReserva.ObtenerReservasPorPersona(id);
        if (reservas.Any()) // Verificar si la persona tiene reservas
        {
            throw new OperacionInvalidaException ("No se puede eliminar la persona porque tiene reservas asociadas");
        }
        _repositorioPersona.EliminarPersona(id);  // Si pasa las validaciones, elimino
    }

}

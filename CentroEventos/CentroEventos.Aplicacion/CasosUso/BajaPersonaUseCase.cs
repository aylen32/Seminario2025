using System;

namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;



public class BajaPersonaUseCase
{
     private readonly IRepositorioPersona _repositorioPersona;
     private readonly IRepositorioReserva _repositorioReserva;
    private readonly IValidadorPersona _validadorPersona;
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;

    public BajaPersonaUseCase(IRepositorioPersona repositorioPersona, IValidadorPersona validadorPersona, IRepositorioReserva repositorioReserva, IRepositorioEventoDeportivo repositorioEventoDeportivo)
    {
        _repositorioReserva = repositorioReserva;       //Inyeccion de dependencia por constructor (supuestamente se puede)
        _repositorioPersona = repositorioPersona;     //Inyeccion de dependencia por constructor (supuestamente se puede)
        _validadorPersona = validadorPersona;
        _repositorioEventoDeportivo = repositorioEventoDeportivo;      //No se si esto estaria bien! ¡Consultar!
    }

    public void Ejecutar(int id){
        IEnumerable<EventoDeportivo> eventos = _repositorioEventoDeportivo.ObtenerTodos();
        foreach (var evento in eventos)
        {
            if (evento.ResponsableId == id) // Verificar si la persona tiene eventos a su nombre
            {
                throw new Exception("No se puede eliminar la persona porque es responsable de algún evento.");
            }
        }
        var persona = _repositorioPersona.ObtenerPersona(id);
        IEnumerable<Reserva> reservas = _repositorioReserva.ObtenerReservasPorPersona(id);
        if (reservas.Any()) // Verificar si la persona tiene reservas
        {
            throw new Exception("No se puede eliminar la persona porque tiene reservas asociadas.");
        }
        _repositorioPersona.EliminarPersona(id);  // Si pasa las validaciones, elimino
    }

}

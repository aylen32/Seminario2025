using System;

namespace CentroEventos.Aplicacion.Validaciones;

public interface IRepositorioEventoDeportivo
{
    bool ExisteEventoPorId(int id);                // Verifica si un evento deportivo con ese ID existe
    EventoDeportivo ? ObtenerEvento(int id);         // Obtiene un evento deportivo con el ID
    IEnumerable<EventoDeportivo> ObtenerTodos();   // Obtiene todos los eventos deportivos
    void AgregarEvento(EventoDeportivo evento);    // Agrega un evento deportivo
    void ModificarEvento(EventoDeportivo evento);  // Modifica un evento deportivo
    void EliminarEvento(int id);                   // Elimina un evento deportivo por ID
}

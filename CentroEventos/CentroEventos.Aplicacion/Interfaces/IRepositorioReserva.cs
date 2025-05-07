using System;

namespace CentroEventos.Aplicacion.Validaciones;

public interface IRepositorioReserva
{
    bool ExisteReservaPorId(int id);                                     // Verifica si una reserva con ese ID existe
    Reserva ObtenerReserva(int id);                                      // Obtiene una reserva con el ID
    IEnumerable<Reserva> ObtenerReservasPorPersona(int personaId);       // Obtiene todas las reservas de una persona
    IEnumerable<Reserva> ObtenerReservasPorEvento(int eventoId);         // Obtiene todas las reservas de un evento
    bool ExisteReservaDuplicada(int personaId, int eventoId);            // Verifica que una persona no tenga una reserva
    void AgregarReserva(Reserva reserva);                                // Agrega una reserva
    void ModificarReserva(Reserva reserva);                              // Modifica una reserva
    void EliminarReserva(int id);                                        // Elimina una reserva por ID
}
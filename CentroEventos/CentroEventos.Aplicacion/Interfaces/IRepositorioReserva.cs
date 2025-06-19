using System;

namespace CentroEventos.Aplicacion.Interfaces;

using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Entidades;


public interface IRepositorioReserva
{
    bool ExisteReservaPorId(int id);                              // Verifica si una reserva con ese ID existe
    Reserva? ObtenerReserva(int id);                              // Obtiene una reserva con el ID
    List<Reserva> ObtenerReservasPorPersona(int personaId);       // Obtiene todas las reservas de una persona
    List<Reserva> ObtenerReservasPorEvento(int eventoId);         // Obtiene todas las reservas por evento
    List<Reserva> ObtenerTodas();                                 // Obtiene todas las reservas de un evento
    bool ExisteReservaDuplicada(int personaId, int eventoId);     // Verifica que una persona no tenga una reserva
    void AgregarReserva(Reserva reserva);                         // Agrega una reserva
    bool ModificarReserva(int id, EstadoAsistencia estado);       // Modifica una reserva
    bool EliminarReserva(int id);                                 // Elimina una reserva por ID
}
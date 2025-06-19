using System;

namespace CentroEventos.Aplicacion.Entidades;

using CentroEventos.Aplicacion.Enumerativos;

public class Reserva
{
    protected Reserva() { }
    public static Reserva CrearNueva() => new Reserva();
    public int Id { get; set; }
    public int PersonaId {get;set;}
    public int EventoDeportivoId {get;set;}
    public DateTime FechaAltaReserva { get; set; } = DateTime.Now;
    public EstadoAsistencia Estado { get; set; } 

    public override string ToString()
    {
        return $"[{Id}] PersonaId: {PersonaId}, EventoId: {EventoDeportivoId}, Alta: {FechaAltaReserva}, Estado: {Estado}";
    }
}
using System;

namespace CentroEventos.Aplicacion;

public class Reserva
{
    public int Id {get;set;}
    public int PersonaId {get;set;}
    public int EventoDeportivoId {get;set;}
    public DateTime FechaAltaReserva {get;set;}
    public EstadoAsistencia Estado { get; set; } 

    public enum EstadoAsistencia
    {
        Pendiente,
        Presente,
        Ausente
    }
    
    public override string ToString()
    {
        return $"[{Id}] PersonaId: {PersonaId}, EventoId: {EventoDeportivoId}, Alta: {FechaAltaReserva}, Estado: {Estado}";
    }
}
using System;

namespace CentroEventos.Aplicacion.Entidades;

public class EventoDeportivo
{
    protected EventoDeportivo(){}
    public static EventoDeportivo CrearNuevo() => new EventoDeportivo();
    public int Id { get; set; }
    public string Nombre {get;set;}="";
    public string Descripcion {get;set;}="";
    public DateTime FechaHoraInicio { get; set; } = DateTime.Now;
    public double DuracionHoras {get;set;}
    public int CupoMaximo {get;set;}
    public int ResponsableId {get;set;}

    public override string ToString()
    {
        return $"[{Id}] {Nombre} ({Descripcion}) - Inicio: {FechaHoraInicio}, Duración: {DuracionHoras} hs, Cupo: {CupoMaximo}, ResponsableId: {ResponsableId}";
    }
}
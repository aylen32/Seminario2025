using System;

namespace CentroEventos.Aplicacion;

public class Persona
{
    public int Id {get;set;}
    public string DNI {get; set;}="";     //Especifica que debe ser de tipo string
    public string Nombre {get;set;}="";
    public string Apellido {get;set;}="";
    public string Telefono {get;set;}="";
    public string  Mail{get;set;}="";

    public override string ToString()
    {
        return $"[{Id}] {Nombre} {Apellido} - DNI: {DNI}, Email: {Mail}, Tel: {Telefono}";
    }
}
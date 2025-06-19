using System;
using System.Collections.Generic;
using System.Linq;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Repositorios;

public class RepositorioEventoDeportivo : IRepositorioEventoDeportivo
{
    private readonly CentroEventosContext _context;

    public RepositorioEventoDeportivo(CentroEventosContext context)
    {
        _context = context;
    }

    public bool ExisteEventoPorId(int id)
    {
        return _context.EventosDeportivos.Any(e => e.Id == id);
    }

    public EventoDeportivo? ObtenerEvento(int id)
    {
        return _context.EventosDeportivos.Find(id);
    }

    public List<EventoDeportivo> ObtenerTodos()
    {
        return _context.EventosDeportivos.ToList();
    }

    public void AgregarEvento(EventoDeportivo evento)
    {
        if (evento == null) 
            return; 

        _context.EventosDeportivos.Add(evento);
        _context.SaveChanges();
    }

    public bool ModificarEvento(int id, string nombre, string descripcion, DateTime fecha, double duracion, int cupo)
    {
        var evento = _context.EventosDeportivos.Find(id);
        if (evento == null)
            return false;

        evento.Nombre = nombre;
        evento.Descripcion = descripcion;
        evento.FechaHoraInicio = fecha;
        evento.DuracionHoras = duracion;
        evento.CupoMaximo = cupo;

        _context.SaveChanges();
        return true;
    }

    public bool EliminarEvento(int id)
    {
        var evento = _context.EventosDeportivos.Find(id);
        if (evento == null)
            return false;

        _context.EventosDeportivos.Remove(evento);
        _context.SaveChanges();
        return true;
    }
}
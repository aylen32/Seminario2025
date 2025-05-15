using System;
using CentroEventos.Aplicacion;
using CentroEventos.Aplicacion.Validaciones;

namespace CentroEventos.Repositorios;

public class RepositorioEventoDeportivo : IRepositorioEventoDeportivo
{
    public void AgregarEvento(EventoDeportivo evento)
    {
        throw new NotImplementedException();
    }

    public void EliminarEvento(int id)
    {
        throw new NotImplementedException();
    }

    public bool ExisteEventoPorId(int id)
    {
        throw new NotImplementedException();
    }

    public void ModificarEvento(EventoDeportivo evento)
    {
        throw new NotImplementedException();
    }

    public EventoDeportivo ObtenerEvento(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<EventoDeportivo> ObtenerTodos()
    {
        throw new NotImplementedException();
    }
}

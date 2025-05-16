using System;

namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
public class ListadoEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;

    public ListadoEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo; // Inyeccion de dependencia por constructor (supuestamente se puede)
    }

    public IEnumerable<EventoDeportivo> ? Ejecutar()
    {
        return _repositorioEventoDeportivo.ObtenerTodos();
    }
}

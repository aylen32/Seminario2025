using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosUso;

public class ObtenerEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento;

    public ObtenerEventoDeportivoUseCase(IRepositorioEventoDeportivo repo)
    {
        _repositorioEvento = repo;
    }

    public EventoDeportivo? Ejecutar(int Idevento)
    {
        return _repositorioEvento.ObtenerEvento(Idevento);
    }
}
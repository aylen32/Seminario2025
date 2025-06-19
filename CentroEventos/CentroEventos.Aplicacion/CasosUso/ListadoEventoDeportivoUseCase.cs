using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosUso;
public class ListadoEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;

    public ListadoEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo;                          
    }

    public List<EventoDeportivo> Ejecutar()
    {
        return _repositorioEventoDeportivo.ObtenerTodos();
    }
}
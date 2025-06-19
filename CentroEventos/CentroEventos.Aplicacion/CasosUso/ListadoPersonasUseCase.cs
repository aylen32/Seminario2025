using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosUso;

public class ListadoPersonasUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    public ListadoPersonasUseCase(IRepositorioPersona repositorioPersona)
    {
        _repositorioPersona = repositorioPersona;
    }

    public List<Persona> Ejecutar()
    {
        return _repositorioPersona.ObtenerTodas(); 
    }
}

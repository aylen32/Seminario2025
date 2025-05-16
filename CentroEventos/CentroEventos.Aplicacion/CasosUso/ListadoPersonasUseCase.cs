using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
namespace CentroEventos.Aplicacion.CasosUso;

public class ListadoPersonasUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    public ListadoPersonasUseCase(IRepositorioPersona repositorioPersona)
    {
        _repositorioPersona = repositorioPersona; // Inyeccion de dependencia por constructor (supuestamente se puede)
    }

    public IEnumerable<Persona> Ejecutar()
    {
        return _repositorioPersona.ObtenerTodas(); // Obtener la lista de personas del repositorio
    }
}

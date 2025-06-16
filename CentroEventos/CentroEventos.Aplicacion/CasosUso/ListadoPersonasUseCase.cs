using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosUso;

public class ListadoPersonasUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IServicioAutorizacion _autorizacion;
    public ListadoPersonasUseCase(IRepositorioPersona repositorioPersona, IServicioAutorizacion autorizacion)
    {
        _repositorioPersona = repositorioPersona;
        _autorizacion = autorizacion;
    }

    public List<Persona> Ejecutar()
    {
        return _repositorioPersona.ObtenerTodas(); // Obtener la lista de personas del repositorio
    }
}

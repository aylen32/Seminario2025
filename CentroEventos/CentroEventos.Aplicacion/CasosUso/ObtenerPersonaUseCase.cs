using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.CasosUso;

public class ObtenerPersonaUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;

    public ObtenerPersonaUseCase(IRepositorioPersona repo)
    {
        _repositorioPersona = repo;
    }

    public Persona? Ejecutar(int Idpersona)
    {
        return _repositorioPersona.ObtenerPersona(Idpersona);
    }
}
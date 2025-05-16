using System;

namespace CentroEventos.Aplicacion.CasosUso;

public class ListarPersonasUseCase
{
    private readonly IRepositorioPersona _repo;

    public ListarPersonasUseCase(IRepositorioPersona repo)
    {
        _repo = repo;
    }

    public IEnumerable<Persona> Ejecutar()
    {
        return _repo.ObtenerTodas();
    }
}
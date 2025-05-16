using System;

namespace CentroEventos.Aplicacion.CasosUso;

public class EliminarPersonaUseCase
{
    private readonly IRepositorioPersona _repo;
    public EliminarPersonaUseCase(IRepositorioPersona repo)
    {
        _repo = repo;
    }

    public void Ejecutar(int id)
    {
        if (!_repo.ExistePersonaPorId(id))
        {
            throw new EntidadNoEncontradaException($"La persona con ID {id} no existe.");
        }
        _repo.EliminarPersona(id);
    }
}

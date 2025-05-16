using System;

namespace CentroEventos.Aplicacion.CasosUso;

using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;

public class ModificarPersonaUseCase
{
    private readonly IRepositorioPersona _repo;
    private readonly IValidadorPersona _validador;

    public ModificarPersonaUseCase(IRepositorioPersona repo, IValidadorPersona validador)
    {
        _repo = repo;
        _validador = validador;
    }

    public void Ejecutar(Persona persona)
    {
        if (!_repo.ExistePersonaPorId(persona.Id))
        { 
            throw new EntidadNoEncontradaException($"La persona con ID {persona.Id} no existe.");
        }
        _validador.Validar(persona); 
        _repo.ModificarPersona(persona);
    }
}

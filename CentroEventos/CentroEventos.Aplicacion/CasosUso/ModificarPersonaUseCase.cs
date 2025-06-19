using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosUso;
public class ModificarPersonaUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IValidadorPersona _validadorPersona;

    public ModificarPersonaUseCase(IRepositorioPersona repo, IValidadorPersona validador)
    {
        _repositorioPersona = repo;
        _validadorPersona = validador;
    }

    public void Ejecutar(Persona persona)
    {
        if (!_repositorioPersona.ExistePersonaPorId(persona.Id))
            throw new EntidadNotFoundException($"La persona con ID {persona.Id} no existe");

        if (!_validadorPersona.Validar(persona))
            throw new ValidacionException(_validadorPersona.ObtenerError() ?? "Error desconocido en la validación");

        _repositorioPersona.ModificarPersona(persona);
    }
}
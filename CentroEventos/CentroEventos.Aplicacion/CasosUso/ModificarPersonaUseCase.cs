using System;

namespace CentroEventos.Aplicacion.CasosUso;

using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Enumerativos;

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
        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.UsuarioModificacion))
        {
            throw new FalloAutorizacionException("No tiene permiso para modificar personas");
        }
        if (!_repo.ExistePersonaPorId(persona.Id))
        {
            throw new EntidadNotFoundException($"La persona con ID {persona.Id} no existe");
        }
        _validador.Validar(persona); 
        _repo.ModificarPersona(persona);
    }
}

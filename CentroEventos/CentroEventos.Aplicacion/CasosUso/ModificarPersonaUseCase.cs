using System;

namespace CentroEventos.Aplicacion.CasosUso;

using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Entidades;

public class ModificarPersonaUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IValidadorPersona _validadorPersona;
    private readonly IServicioAutorizacion _autorizacion;

    public ModificarPersonaUseCase(IRepositorioPersona repo, IValidadorPersona validador, IServicioAutorizacion autorizacion)
    {
        _repositorioPersona = repo;
        _validadorPersona = validador;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Persona persona, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioModificacion))
            throw new FalloAutorizacionException("No tiene permiso para modificar personas");

        if (!_repositorioPersona.ExistePersonaPorId(persona.Id))
            throw new EntidadNotFoundException($"La persona con ID {persona.Id} no existe");

        if (!_validadorPersona.Validar(persona))
            throw new ValidacionException(_validadorPersona.ObtenerError() ?? "Error desconocido en la validación");

        _repositorioPersona.ModificarPersona(persona);
    }
}


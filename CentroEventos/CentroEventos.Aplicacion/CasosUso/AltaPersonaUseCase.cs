using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Servicio;

public class AltaPersonaUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IValidadorPersona _validadorPersona;
    private readonly IServicioAutorizacion _autorizacion;

    public AltaPersonaUseCase(
        IRepositorioPersona repositorioPersona,
        IValidadorPersona validadorPersona,
        IServicioAutorizacion autorizacion)
    {
        _repositorioPersona = repositorioPersona;
        _validadorPersona = validadorPersona;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Persona persona, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, PermisoTipo.PersonaAlta))
            throw new OperacionInvalidaException("No tiene permiso para dar de alta personas");

        if (!_validadorPersona.Validar(persona))
            throw new ValidacionException(_validadorPersona.ObtenerError() ?? "Error en la validación de la persona");

        _repositorioPersona.AgregarPersona(persona);
    }
}
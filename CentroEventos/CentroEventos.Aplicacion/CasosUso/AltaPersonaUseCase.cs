using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosUso;
public class AltaPersonaUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IValidadorPersona _validadorPersona;

    public AltaPersonaUseCase( IRepositorioPersona repositorioPersona, IValidadorPersona validadorPersona)
    {
        _repositorioPersona = repositorioPersona;
        _validadorPersona = validadorPersona;
    }

    public void Ejecutar(Persona persona)
    {
        if (!_validadorPersona.Validar(persona))
            throw new ValidacionException(_validadorPersona.ObtenerError() ?? "Error en la validación de la persona");

        _repositorioPersona.AgregarPersona(persona);
    }
}
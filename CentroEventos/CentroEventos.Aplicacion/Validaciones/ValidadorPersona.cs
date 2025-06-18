using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Validaciones;

public class ValidadorPersona : IValidadorPersona
{
    private readonly IRepositorioPersona _personaRepo;
    private string? _error;

    public ValidadorPersona(IRepositorioPersona personaRepo)
    {
        _personaRepo = personaRepo;
    }


    public bool Validar(Persona persona)
    {
        _error = "";

        if (string.IsNullOrWhiteSpace(persona.Nombre))
        {
            _error += "El Nombre no puede estar vacío. ";
        }

        if (string.IsNullOrWhiteSpace(persona.Apellido))
        {
            _error += "El Apellido no puede estar vacío. ";
        }

        if (string.IsNullOrWhiteSpace(persona.DNI))
        {
            _error += "El DNI no puede estar vacío. ";
        }

        if (string.IsNullOrWhiteSpace(persona.Mail))
        {
            _error += "El Email no puede estar vacío. ";
        }

        if (_personaRepo.ExistePersonaPorDni(persona.DNI, persona.Id))
        {
            _error += "La persona con este DNI ya está registrada. ";
        }

        if (_personaRepo.ExistePersonaPorEmail(persona.Mail, persona.Id))
        {
            _error += "La persona con este Email ya está registrada. ";
        }

        if (!string.IsNullOrWhiteSpace(_error))
            _error = _error.TrimEnd('.', ' ');

        return string.IsNullOrWhiteSpace(_error);
    }
    public string? ObtenerError()
    {
        return _error;
    }
}   
using System;

namespace CentroEventos.Aplicacion.Interfaces;

public interface IValidadorPersona
{
    bool Validar(Persona persona);
    string? ObtenerError();  // Para obtener el mensaje del último error de validación
}
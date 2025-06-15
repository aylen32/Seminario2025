using System;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Interfaces;

public interface IValidadorUsuario
{
    bool Validar(Usuario usuario);
    string? ObtenerError();
}

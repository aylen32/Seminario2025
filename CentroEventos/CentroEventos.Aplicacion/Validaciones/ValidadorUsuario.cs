using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.Validaciones;

public class ValidadorUsuario : IValidadorUsuario
{
    private readonly IRepositorioUsuario _usuarioRepo;
    private string? _error;

    public ValidadorUsuario(IRepositorioUsuario usuarioRepo)
    {
        _usuarioRepo = usuarioRepo;
    }
    public string? ObtenerError() => _error;
    public bool Validar(Usuario usuario)
    {
        if (string.IsNullOrWhiteSpace(usuario.Nombre))
        {
            _error = "El nombre es obligatorio.";
            return false;
        }
        return true;
    }
}

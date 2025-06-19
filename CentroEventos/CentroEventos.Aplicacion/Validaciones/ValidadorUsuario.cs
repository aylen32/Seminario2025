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
            _error = "";
        if (string.IsNullOrWhiteSpace(usuario.Nombre))
        {
            _error += "El nombre es obligatorio.";
         
        }
        if (string.IsNullOrWhiteSpace(usuario.Apellido))
        {
            _error += "El apellido es obligatorio.";
        
        }
        if (string.IsNullOrWhiteSpace(usuario.CorreoElectronico))
        {
            _error += "El Email no puede estar vacío. ";
        }
        if (string.IsNullOrWhiteSpace(usuario.HashContrasenia))
        {
            _error += "La constraseña no puede estar en blanco. ";
        }
        if (!string.IsNullOrWhiteSpace(_error))
            _error = _error.TrimEnd('.', ' ');

        return string.IsNullOrWhiteSpace(_error);     
    }
}
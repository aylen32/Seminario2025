using System;

namespace CentroEventos.Aplicacion.Entidades;

public class Usuario
{
    protected Usuario() { }
    public static Usuario CrearNuevo() => new Usuario();

    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? HashContrasenia { get; set; }

    public List<UsuarioPermiso> Permisos { get; set; } = new(); 
}
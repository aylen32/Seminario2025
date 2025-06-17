using System;

namespace CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;

public class Usuario
{
    public Usuario() { }
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? HashContrasenia { get; set; }


    public Permiso Permisos { get; set; } = Permiso.Ninguno;
}
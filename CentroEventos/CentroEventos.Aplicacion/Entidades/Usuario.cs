using System;

namespace CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;

public class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? HashContrasenia { get; set; }

    public string? Salt { get; set; }

    public List<Permiso>? Permisos { get; set; }
}

using System;

namespace CentroEventos.Aplicacion.Entidades;

using CentroEventos.Aplicacion.Enumerativos;

public class UsuarioPermiso
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public Permiso Permiso { get; set; }
}
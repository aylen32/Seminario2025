using System;

namespace CentroEventos.Aplicacion.Entidades;

public class UsuarioPermiso
{
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    public int PermisoId { get; set; }
    public Permiso? Permiso { get; set; }
}
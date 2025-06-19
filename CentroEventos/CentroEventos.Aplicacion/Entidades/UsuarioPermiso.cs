using System;

namespace CentroEventos.Aplicacion.Entidades;

public class UsuarioPermiso
{
    protected UsuarioPermiso() { }
    public UsuarioPermiso(int usuarioId, int permisoId)
    {
        UsuarioId = usuarioId;
        PermisoId = permisoId;
    }
    
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    public int PermisoId { get; set; }
    public Permiso? Permiso { get; set; }
}
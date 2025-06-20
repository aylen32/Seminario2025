using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.Servicio;

public class ServicioAutorizacion : IServicioAutorizacion
{
    private readonly IRepositorioUsuario _repoUsuarios;

    public ServicioAutorizacion(IRepositorioUsuario repoUsuarios)
    {
        _repoUsuarios = repoUsuarios;
    }

    public bool EsAdministrador(int usuarioId)
    {
        var usuario = _repoUsuarios.ObtenerUsuario(usuarioId);
        if (usuario == null)
            return false;

        return usuario.Id == 1;
    }

    // Devuelve true si tiene al menos un permiso de gestión
    public bool TieneAlgunoPermisoGestion(int usuarioId)
    {
        var usuario = _repoUsuarios.ObtenerUsuario(usuarioId);
        if (usuario == null) return false;

        return usuario.Permisos.Any(p => p.Permiso != null &&
            (p.Permiso.Tipo == PermisoTipo.UsuarioAlta ||
             p.Permiso.Tipo == PermisoTipo.UsuarioModificacion ||
             p.Permiso.Tipo == PermisoTipo.UsuarioBaja));
    }

    // Devuelve true si tiene permiso específico de Alta
    public bool TienePermisoAlta(int usuarioId)
    {
        return VerificarPermisoInterno(usuarioId, PermisoTipo.UsuarioAlta);
    }

    // Devuelve true si tiene permiso específico de Modificación
    public bool TienePermisoModificacion(int usuarioId)
    {
        return VerificarPermisoInterno(usuarioId, PermisoTipo.UsuarioModificacion);
    }

    // Devuelve true si tiene permiso específico de Baja
    public bool TienePermisoBaja(int usuarioId)
    {
        return VerificarPermisoInterno(usuarioId, PermisoTipo.UsuarioBaja);
    }

    // Devuelve true si tiene los tres permisos de gestión
    public bool TieneTodosLosPermisosGestion(int usuarioId)
    {
        var usuario = _repoUsuarios.ObtenerUsuario(usuarioId);
        if (usuario == null) return false;

        var permisos = usuario.Permisos.Select(p => p.Permiso?.Tipo).ToHashSet();

        return permisos.Contains(PermisoTipo.UsuarioAlta)
            && permisos.Contains(PermisoTipo.UsuarioModificacion)
            && permisos.Contains(PermisoTipo.UsuarioBaja);
    }

    private bool VerificarPermisoInterno(int usuarioId, PermisoTipo permisoTipo)
    {
        var usuario = _repoUsuarios.ObtenerUsuario(usuarioId);
        if (usuario == null) return false;

        return usuario.Permisos.Any(p => p.Permiso != null && p.Permiso.Tipo == permisoTipo);
    }
    
    public bool TienePermiso(int usuarioId, PermisoTipo permisoTipo)
    {
      var usuario = _repoUsuarios.ObtenerUsuario(usuarioId);
      if (usuario == null) return false;

      return usuario.Permisos.Any(p => p.Permiso != null && p.Permiso.Tipo == permisoTipo);
     }

}
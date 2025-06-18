using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.Servicio
{
  public class ServicioAutorizacionProvisorio : IServicioAutorizacion
  {
    private readonly IRepositorioUsuario _repositorioUsuario;

    public ServicioAutorizacionProvisorio(IRepositorioUsuario repositorioUsuario)
    {
        _repositorioUsuario = repositorioUsuario;
    }

    public bool PoseeElPermiso(int idUsuario, PermisoTipo permisoTipo)
    {
      if (EsAdministrador(idUsuario))
        return true;

      var usuario = _repositorioUsuario.ObtenerUsuarioConPermisos(idUsuario);
      if (usuario == null || usuario.Permisos == null)
        return false;

      return usuario.Permisos.Any(up => up.Permiso != null && up.Permiso.Tipo == permisoTipo);
    }


    public bool EsAdministrador(int idUsuario)
    {
        return idUsuario == 1;
    }

    public bool TienePermisoDeGestion(int idUsuario)
    {
        var permisosGestion = new[]
        {
            PermisoTipo.UsuarioAlta,
            PermisoTipo.UsuarioBaja,
            PermisoTipo.UsuarioModificacion,
            PermisoTipo.EventoAlta,
            PermisoTipo.EventoBaja,
            PermisoTipo.EventoModificacion,
            PermisoTipo.ReservaAlta,
            PermisoTipo.ReservaBaja,
            PermisoTipo.ReservaModificacion
        };

        var usuario = _repositorioUsuario.ObtenerUsuarioConPermisos(idUsuario);
        if (usuario == null)
            return false;

        return usuario.Permisos.Any(up => up.Permiso != null && permisosGestion.Contains(up.Permiso.Tipo));
    }
  }
}
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.Servicio
{
  public class ServicioAutorizacion : IServicioAutorizacion
  {
      private readonly IRepositorioUsuario _repositorioUsuario;

      public ServicioAutorizacion(IRepositorioUsuario repositorioUsuario)
      {
        _repositorioUsuario = repositorioUsuario;
      }

      public bool PoseeElPermiso(int idUsuario, PermisoTipo permisoTipo)
      {
        var usuario = _repositorioUsuario.ObtenerUsuarioConPermisos(idUsuario);
        if (usuario == null || usuario.Permisos == null)
            return false;

        return usuario.Permisos.Any(up => up.Permiso != null && up.Permiso.Tipo == permisoTipo);
      }
  }
}
using System.Linq;
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

      public bool PoseeElPermiso(int idUsuario, Permiso permiso)
      {
        // El usuario con ID = 1 siempre tiene todos los permisos (Ya que el id es incremental)
         if (idUsuario == 1)
            return true;

        var usuario = _repositorioUsuario.ObtenerUsuario(idUsuario);
        if (usuario == null) return false;

        return usuario.UsuarioPermisos.Any(up => up.Permiso == permiso);
      }
    }
}
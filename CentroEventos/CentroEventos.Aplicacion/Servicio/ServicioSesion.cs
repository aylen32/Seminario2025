using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;

namespace CentroEventos.Aplicacion.Servicio
{
    public class ServicioSesion : IServicioSesion
    {
        private Usuario? _usuario;
        public Usuario? UsuarioActual => _usuario;
        public bool EstaAutenticado => _usuario!=null;

        public List<PermisoTipo> Permisos =>
          _usuario?.Permisos?
          .Where(up => up.Permiso != null)
          .Select(up => up.Permiso!.Tipo)
          .ToList() ?? new();
          
        public void CerrarSesion()
        {
            _usuario = null;
        }

        public void IniciarSesion(Usuario usuario)
        {
            _usuario = usuario;
        }
    }
}



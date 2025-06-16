using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
namespace CentroEventos.Aplicacion.Servicio{
        public class ServicioSesion: IServicioSesion   {
        private Usuario? _usuario;

        public Usuario? UsuarioActual => _usuario;

        public void IniciarSesion(Usuario usuario)
        {
            _usuario = usuario;
        }

        public void CerrarSesion()
        {
            _usuario = null;
        }
        public bool EstaAutenticado => _usuario != null;
        public List<Permiso>? GetPermisos()
        {
            return null;
        }
    }
}
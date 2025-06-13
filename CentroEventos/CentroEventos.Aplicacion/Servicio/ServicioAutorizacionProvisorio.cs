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

        public bool PoseeElPermiso(int idUsuario, Permiso permiso)
        {
           var usuario = _repositorioUsuario.ObtenerUsuario(idUsuario);
           if (usuario == null)
             return false;

           return (usuario.Permisos & permiso) == permiso;
        }
    }
}
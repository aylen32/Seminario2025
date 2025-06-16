using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;

namespace CentroEventos.Aplicacion.Interfaces
{
    public interface IRepositorioUsuario
    {
        void AgregarUsuario(Usuario usuario);
        void EliminarUsuario(int id);
        void ModificarUsuario(Usuario usuario);
        Usuario? ObtenerUsuario(int id);
        List<Usuario> ObtenerTodos();
        bool ExisteUsuarioPorId(int id);
        void AgregarPermiso(int usuarioId, Permiso permiso);
        void EliminarPermiso(int usuarioId, Permiso permiso);
    }
}

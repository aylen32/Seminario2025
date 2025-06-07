using System.Collections.Generic;
using System.Linq;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;
using CentroEventos.Aplicacion;
using CentroEventos.Aplicacion.Enumerativos; 


namespace CentroEventos.Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private readonly CentroEventosContext _context;

        public RepositorioUsuario(CentroEventosContext context)
        {
            _context = context;
        }

        public void AgregarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void EliminarUsuario(int id)
        {
            var usuario = _context.Usuarios.Include(u => u.UsuarioPermisos).FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                throw new EntidadNotFoundException($"El usuario con ID {id} no existe.");

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }

        public void ModificarUsuario(Usuario usuario)
        {
            var existente = _context.Usuarios.Include(u => u.UsuarioPermisos).FirstOrDefault(u => u.Id == usuario.Id);
            if (existente == null)
                throw new EntidadNotFoundException($"El usuario con ID {usuario.Id} no existe.");

            existente.Nombre = usuario.Nombre;
            existente.Apellido = usuario.Apellido;
            existente.CorreoElectronico = usuario.CorreoElectronico;
            existente.HashContrasenia = usuario.HashContrasenia;
            existente.Salt = usuario.Salt;

            // Reemplazar permisos si es necesario
            existente.UsuarioPermisos = usuario.UsuarioPermisos;

            _context.SaveChanges();
        }

        public Usuario? ObtenerUsuario(int id)
        {
            return _context.Usuarios.Include(u => u.UsuarioPermisos).FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Usuario> ObtenerTodos()
        {
            return _context.Usuarios.Include(u => u.UsuarioPermisos).ToList();
        }

        public bool ExisteUsuarioPorId(int id)
        {
            return _context.Usuarios.Any(u => u.Id == id);
        }

        public void AgregarPermiso(int usuarioId, Permiso permiso)
        {
            var usuario = _context.Usuarios.Include(u => u.UsuarioPermisos).FirstOrDefault(u => u.Id == usuarioId);
            if (usuario == null)
               throw new EntidadNotFoundException($"Usuario con ID {usuarioId} no existe");

            // Verifico que no tenga ya ese permiso
            if (usuario.UsuarioPermisos.Any(p => p.Permiso == permiso))
                throw new OperacionInvalidaException("El usuario ya posee ese permiso");

            var usuarioPermiso = new UsuarioPermiso
            {
              UsuarioId = usuarioId,
              Permiso = permiso,
              Usuario = usuario
            };
             usuario.UsuarioPermisos.Add(usuarioPermiso);
             _context.SaveChanges();
        }

        public void EliminarPermiso(int usuarioId, Permiso permiso)
        {
           var usuarioPermiso = _context.UsuarioPermisos.FirstOrDefault(up => up.UsuarioId == usuarioId && up.Permiso == permiso);
            if (usuarioPermiso == null)
                throw new OperacionInvalidaException("El usuario no posee ese permiso");
           _context.UsuarioPermisos.Remove(usuarioPermiso);
           _context.SaveChanges();
        }
    }
}

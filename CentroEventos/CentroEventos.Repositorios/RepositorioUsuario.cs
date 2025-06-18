using System.Collections.Generic;
using System.Linq;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        var usuario = _context.Usuarios
            .Include(u => u.Permisos)
            .FirstOrDefault(u => u.Id == id);

        if (usuario != null)
        {
          _context.Usuarios.Remove(usuario);
          _context.SaveChanges();
        }
      }

      public void ModificarUsuario(Usuario usuario)
      {
        var existente = _context.Usuarios
            .Include(u => u.Permisos)
            .FirstOrDefault(u => u.Id == usuario.Id);

        if (existente != null)
        {
          existente.Nombre = usuario.Nombre;
          existente.Apellido = usuario.Apellido;
          existente.CorreoElectronico = usuario.CorreoElectronico;
          existente.HashContrasenia = usuario.HashContrasenia;

          _context.SaveChanges();
        }
      }

      public Usuario? ObtenerUsuario(int id) =>
          _context.Usuarios.FirstOrDefault(u => u.Id == id);

      public Usuario? ObtenerUsuarioConPermisos(int id) =>
          _context.Usuarios
              .Include(u => u.Permisos)
              .ThenInclude(p => p.Permiso)
              .FirstOrDefault(u => u.Id == id);

      public List<Usuario> ObtenerTodos() =>
          _context.Usuarios.ToList();

      public bool ExisteUsuarioPorId(int id) =>
          _context.Usuarios.Any(u => u.Id == id);

      public void AgregarPermiso(int usuarioId, Permiso permiso)
      {
        var usuario = _context.Usuarios
            .Include(u => u.Permisos)
            .FirstOrDefault(u => u.Id == usuarioId);

        if (usuario == null)
          return;

        if (!usuario.Permisos.Any(p => p.PermisoId == permiso.Id))
        {
          usuario.Permisos.Add(new UsuarioPermiso(usuarioId, permiso.Id));
          _context.SaveChanges();
        }
      }

      public void EliminarPermiso(int usuarioId, Permiso permiso)
      {
        var usuario = _context.Usuarios
            .Include(u => u.Permisos)
            .FirstOrDefault(u => u.Id == usuarioId);

        if (usuario == null)
          return;

        var permisoAEliminar = usuario.Permisos
            .FirstOrDefault(p => p.PermisoId == permiso.Id);

        if (permisoAEliminar != null)
        {
          usuario.Permisos.Remove(permisoAEliminar);
          _context.SaveChanges();
        }
      }

      public void AsignarPermisos(int usuarioId, List<UsuarioPermiso> permisos)
      {
        var usuario = _context.Usuarios
            .Include(u => u.Permisos)
            .FirstOrDefault(u => u.Id == usuarioId);

        if (usuario == null)
          return;

        usuario.Permisos.Clear();
        foreach (var permiso in permisos)
        {
          usuario.Permisos.Add(new UsuarioPermiso(usuarioId, permiso.PermisoId));
        }

        _context.SaveChanges();
      }

      public int CantidadUsuariosRegistrados() =>
          _context.Usuarios.Count();

      public List<Permiso> ObtenerTodosLosPermisos() =>
          _context.Permisos.ToList();
    }
  }
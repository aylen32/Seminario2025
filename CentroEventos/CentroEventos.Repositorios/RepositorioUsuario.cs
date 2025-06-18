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

      if (usuario == null)
        throw new EntidadNotFoundException($"El usuario con ID {id} no existe");

      _context.Usuarios.Remove(usuario);
      _context.SaveChanges();
    }

    public void ModificarUsuario(Usuario usuario)
    {
      var existente = _context.Usuarios
        .Include(u => u.Permisos)
        .FirstOrDefault(u => u.Id == usuario.Id);

      if (existente == null)
        throw new EntidadNotFoundException($"El usuario con ID {usuario.Id} no existe");

      // Actualizar datos básicos
      existente.Nombre = usuario.Nombre;
      existente.Apellido = usuario.Apellido;
      existente.CorreoElectronico = usuario.CorreoElectronico;
      existente.HashContrasenia = usuario.HashContrasenia;

      // Obtener permisos actuales y nuevos
      var permisosExistentes = existente.Permisos.ToList();
      var permisosIdsNuevos = usuario.Permisos.Select(p => p.PermisoId).ToList();

      // Remover permisos que ya no están en la lista nueva
      foreach (var permisoExistente in permisosExistentes)
      {
        if (!permisosIdsNuevos.Contains(permisoExistente.PermisoId))
        {
          existente.Permisos.Remove(permisoExistente);
        }
      }

      // Agregar permisos nuevos que no estén ya en existente.Permisos
      foreach (var nuevoPermisoId in permisosIdsNuevos)
      {
        if (!permisosExistentes.Any(p => p.PermisoId == nuevoPermisoId))
        {
            existente.Permisos.Add(new UsuarioPermiso(existente.Id, nuevoPermisoId));
        }
      }

      _context.SaveChanges();
    }


    public Usuario? ObtenerUsuario(int id)
    {
      return _context.Usuarios.FirstOrDefault(u => u.Id == id);
    }

    public List<Usuario> ObtenerTodos()
    {
      return _context.Usuarios.ToList();
    }

    public bool ExisteUsuarioPorId(int id)
    {
      return _context.Usuarios.Any(u => u.Id == id);
    }

    public void AgregarPermiso(int usuarioId, Permiso permiso)
    {
      var usuario = _context.Usuarios
        .Include(u => u.Permisos)
        .FirstOrDefault(u => u.Id == usuarioId);

      if (usuario == null)
        throw new EntidadNotFoundException($"Usuario con ID {usuarioId} no existe");

      bool yaTienePermiso = usuario.Permisos.Any(up => up.PermisoId == permiso.Id);
      if (yaTienePermiso)
        throw new OperacionInvalidaException("El usuario ya posee ese permiso");

      var usuarioPermiso = new UsuarioPermiso(usuarioId, permiso.Id);
      usuario.Permisos.Add(usuarioPermiso);
      _context.SaveChanges();
    }


    public void EliminarPermiso(int usuarioId, Permiso permiso)
    {
      var usuario = _context.Usuarios
          .Include(u => u.Permisos)
          .FirstOrDefault(u => u.Id == usuarioId);

      if (usuario == null)
        throw new EntidadNotFoundException($"Usuario con ID {usuarioId} no existe");

      var usuarioPermiso = usuario.Permisos.FirstOrDefault(up => up.PermisoId == permiso.Id);
      if (usuarioPermiso == null)
        throw new OperacionInvalidaException("El usuario no posee ese permiso");

      usuario.Permisos.Remove(usuarioPermiso);
      _context.SaveChanges();
    }

    public Usuario? ObtenerUsuarioConPermisos(int idUsuario)
    {
      return _context.Usuarios
          .Include(u => u.Permisos)
          .ThenInclude(up => up.Permiso)
          .FirstOrDefault(u => u.Id == idUsuario);
    }
  }
}
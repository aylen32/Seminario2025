using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;


public interface IServicioSesion
{
    Usuario? UsuarioActual { get; }
    void IniciarSesion(Usuario usuario);
    void CerrarSesion();
      bool EstaAutenticado { get; }
    List<Permiso>? GetPermisos();
}

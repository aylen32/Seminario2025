using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
namespace CentroEventos.Aplicacion.Servicio;

public interface IServicioSesion
{
    Usuario? UsuarioActual { get; }
    void IniciarSesion(Usuario usuario);
    void CerrarSesion();
    bool EstaAutenticado { get; }

    Permiso Permisos { get; } 
}

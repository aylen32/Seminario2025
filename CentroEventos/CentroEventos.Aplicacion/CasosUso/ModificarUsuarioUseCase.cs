using System;

namespace CentroEventos.Aplicacion.CasosUso;

using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
public class ModificarUsuarioUseCase
{
private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly IValidadorUsuario _validador;

    public ModificarUsuarioUseCase(IRepositorioUsuario repositorioUsuario, IServicioAutorizacion autorizacion, IValidadorUsuario validador)
    {
        _repositorioUsuario = repositorioUsuario;
        _autorizacion = autorizacion;
        _validador = validador;
    }

    public void Ejecutar(Usuario usuario)
    {
        if (!_autorizacion.PoseeElPermiso(usuario.Id, PermisoTipo.UsuarioModificacion))
        {
            throw new UnauthorizedAccessException("No tiene permiso para modificar este usuario.");
        }

        if (!_validador.Validar(usuario))
        {
            throw new ArgumentException(_validador.ObtenerError() ?? "Datos del usuario inválidos.");
        }
    
         _repositorioUsuario.ModificarUsuario(usuario);
    }

}

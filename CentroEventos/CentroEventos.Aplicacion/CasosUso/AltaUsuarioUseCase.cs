using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicio;
namespace CentroEventos.Aplicacion.CasosUso;

public class AltaUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorio;
    private readonly IValidadorUsuario _validador;
    private readonly IServicioAutorizacion _autorizacion;

    public AltaUsuarioUseCase(IRepositorioUsuario repositorio, IValidadorUsuario validador, IServicioAutorizacion autorizacion)
    {
        _repositorio = repositorio;
        _validador = validador;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Usuario usuario, int idSolicitante)
    {
        if (!_validador.Validar(usuario))
            throw new ValidacionException(_validador.ObtenerError() ?? "Usuario inválido");

        // Si no es el primer usuario, verificar permisos del solicitante
        if (_repositorio.CantidadUsuariosRegistrados() > 0 &&
            !_autorizacion.TienePermisoDeGestion(idSolicitante))
        {
            throw new OperacionInvalidaException("No tiene permiso para dar de alta usuarios.");
        }

        _repositorio.AgregarUsuario(usuario);

        if (_repositorio.CantidadUsuariosRegistrados() == 1)
        {
            var permisos = _repositorio.ObtenerTodosLosPermisos()
                .Select(p => new UsuarioPermiso(usuario.Id, p.Id))
                .ToList();

            _repositorio.AsignarPermisos(usuario.Id, permisos);
        }
    }
}

using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicio;
namespace CentroEventos.Aplicacion.CasosUso;

public class AltaUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorio;
    private readonly IValidadorUsuario _validador;

    public AltaUsuarioUseCase(IRepositorioUsuario repositorio, IValidadorUsuario validador)
    {
        _repositorio = repositorio;
        _validador = validador;
    }

    public void Ejecutar(Usuario usuario)
    {
      if (!_validador.Validar(usuario))
        throw new ValidacionException(_validador.ObtenerError() ?? "Usuario inválido");

      _repositorio.AgregarUsuario(usuario); 

      if (_repositorio.CantidadUsuariosRegistrados() == 1)
      {
        var permisos = _repositorio.ObtenerTodosLosPermisos().ToList();
        var lista = permisos.Select(p => new UsuarioPermiso(usuario.Id, p.Id)).ToList();

        _repositorio.AsignarPermisos(usuario.Id, lista);
      }
    }
}
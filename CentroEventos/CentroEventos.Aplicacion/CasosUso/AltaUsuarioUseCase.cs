using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.CasosUso;

public class AltaUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorioUsuario;
  private readonly IValidadorUsuario _validadorUsuario;
  private readonly IServicioAutorizacion _autorizacion;

  public AltaUsuarioUseCase(IRepositorioUsuario repositorioUsuario, IValidadorUsuario validadorUsuario,
    IServicioAutorizacion autorizacion)
  {
    _repositorioUsuario = repositorioUsuario;
    _validadorUsuario = validadorUsuario;
    _autorizacion = autorizacion;
  }

  public void Ejecutar(Usuario usuario)
  {
   

    if (!_validadorUsuario.Validar(usuario))
    {
        throw new ValidacionException(_validadorUsuario.ObtenerError() ?? "Error desconocido en la validación");
    }

    _repositorioUsuario.AgregarUsuario(usuario);
  }

}

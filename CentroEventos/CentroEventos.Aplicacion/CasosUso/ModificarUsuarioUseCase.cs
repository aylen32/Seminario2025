using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.CasosUso;
public class ModificarUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorio;
    private readonly IValidadorUsuario _validador;
    private readonly IServicioAutorizacion _autorizacion;

    public ModificarUsuarioUseCase(IRepositorioUsuario repositorio, IValidadorUsuario validador, IServicioAutorizacion autorizacion)
    {
        _repositorio = repositorio;
        _validador = validador;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Usuario usuarioModificado, int idSolicitante)
    {
        if (!_validador.Validar(usuarioModificado))
            throw new ValidacionException(_validador.ObtenerError() ?? "Usuario inválido");

        var esModificacionPropia = usuarioModificado.Id == idSolicitante;

        if (!esModificacionPropia && !_autorizacion.TienePermisoDeGestion(idSolicitante))
        {
            throw new OperacionInvalidaException("No tiene permiso para modificar otros usuarios.");
        }

        _repositorio.ModificarUsuario(usuarioModificado);
    }
}
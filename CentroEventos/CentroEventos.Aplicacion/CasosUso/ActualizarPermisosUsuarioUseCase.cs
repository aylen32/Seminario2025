using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.CasosUso;

public class ActualizarPermisosUsuarioUseCase
{
    private readonly IRepositorioUsuario _repoUsuarios;
    private readonly IServicioAutorizacion _autorizacion;

    public ActualizarPermisosUsuarioUseCase(IRepositorioUsuario repoUsuarios, IServicioAutorizacion autorizacion)
    {
        _repoUsuarios = repoUsuarios;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(int adminId, int usuarioId, List<PermisoTipo> permisosDeseados)
    {
        // Solo administrador o quien tenga TODOS los permisos de gestion puede modificar permisos de otros usuarios
        if (!_autorizacion.EsAdministrador(adminId) && !_autorizacion.TieneTodosLosPermisosGestion(adminId))
            throw new OperacionInvalidaException("No tenés permiso para modificar los permisos de otros usuarios.");

        var usuario = _repoUsuarios.ObtenerUsuario(usuarioId);
        if (usuario == null)
            throw new EntidadNotFoundException("Usuario no encontrado.");

        var permisosActuales = usuario.Permisos.Select(p => p.Permiso!.Tipo).ToHashSet();
        var permisosNuevos = permisosDeseados.ToHashSet();

        var todosLosPermisos = _repoUsuarios.ObtenerTodosLosPermisos();

        // Quitar los permisos que ya no están seleccionados
        foreach (var tipo in permisosActuales.Except(permisosNuevos))
        {
            var permiso = todosLosPermisos.First(p => p.Tipo == tipo);
            _repoUsuarios.EliminarPermiso(usuarioId, permiso);
        }

        // Agregar los permisos nuevos que no tenía
        foreach (var tipo in permisosNuevos.Except(permisosActuales))
        {
            var permiso = todosLosPermisos.First(p => p.Tipo == tipo);
            _repoUsuarios.AgregarPermiso(usuarioId, permiso);
        }
    }
}
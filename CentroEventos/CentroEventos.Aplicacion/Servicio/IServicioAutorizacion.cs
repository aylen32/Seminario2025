using CentroEventos.Aplicacion.Enumerativos;
namespace CentroEventos.Aplicacion.Servicio;

public interface IServicioAutorizacion
{
    bool EsAdministrador(int usuarioId);
    bool TieneAlgunoPermisoGestion(int usuarioId);
    bool TienePermisoAlta(int usuarioId);
    bool TienePermisoModificacion(int usuarioId);
    bool TienePermisoBaja(int usuarioId);
    bool TieneTodosLosPermisosGestion(int usuarioId);
    bool TienePermiso(int usuarioId, PermisoTipo permiso);
}

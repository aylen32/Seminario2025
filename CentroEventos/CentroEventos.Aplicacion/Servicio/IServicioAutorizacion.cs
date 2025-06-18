using CentroEventos.Aplicacion.Enumerativos;
namespace CentroEventos.Aplicacion.Servicio;

public interface IServicioAutorizacion
{
    bool PoseeElPermiso(int idUsuario, PermisoTipo permisoTipo);
}
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;


public interface IServicioAutorizacion
{
    bool PoseeElPermiso(int idUsuario, Permiso permiso);
}

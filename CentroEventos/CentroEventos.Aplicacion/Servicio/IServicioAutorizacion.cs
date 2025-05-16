using System;

namespace CentroEventos.Aplicacion.Servicio;

public interface IServicioAutorizacion
{
    bool PoseeElPermiso(int idUsuario, Permiso permiso);
}

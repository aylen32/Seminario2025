using System;

namespace CentroEventos.Aplicacion.Servicio;

using CentroEventos.Aplicacion.Enumerativos;

public interface IServicioAutorizacion
{
    bool PoseeElPermiso(int idUsuario, Permisos permiso);
}

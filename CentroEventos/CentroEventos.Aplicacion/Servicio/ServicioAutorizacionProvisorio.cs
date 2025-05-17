using System;

namespace CentroEventos.Aplicacion.Servicio;

using CentroEventos.Aplicacion.Enumerativos;

public class ServicioAutorizacionProvisorio : IServicioAutorizacion
{
    public bool PoseeElPermiso(int idUsuario, Permiso permiso)
    {
        if (idUsuario == 1)     // Usuario con id 1 tiene todos los permisos
        {
            return true;
        }
        else
        {
            return false;      // Los demás usuarios no tienen permisos
        }
    }
}

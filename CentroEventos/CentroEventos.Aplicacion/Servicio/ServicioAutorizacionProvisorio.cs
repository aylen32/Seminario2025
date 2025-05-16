using System;

namespace CentroEventos.Aplicacion.Servicio;

using CentroEventos.Aplicacion.Enumerativos;

public class ServicioAutorizacionProvisorio : IServicioAutorizacion
{
    public bool PoseeElPermiso(int idUsuario, Permiso permiso)
        {
            // Usuario con id 1 tiene todos los permisos
            if (idUsuario == 1)
                return true;

            // Los demás usuarios no tienen permisos
            return false;
        }
}

using System;
using CentroEventos.Aplicacion.Enumerativos;
namespace CentroEventos.Aplicacion.Entidades;

public class Permiso
{
    protected Permiso(){}
    public static Permiso Crear(PermisoTipo tipo)
    {
        return new Permiso { Tipo = tipo };
    }
    public int Id { get; set; }
    public PermisoTipo Tipo { get; set; }
    public List<UsuarioPermiso> Usuarios { get; set; } = new();
}
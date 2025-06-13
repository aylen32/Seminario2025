namespace CentroEventos.Aplicacion.Enumerativos;

[Flags]
public enum Permiso
{
    Ninguno = 0,
    EventoAlta = 1 << 0,
    EventoModificacion = 1 << 1,
    EventoBaja = 1 << 2,
    ReservaAlta = 1 << 3,
    ReservaModificacion = 1 << 4,
    ReservaBaja = 1 << 5,
    UsuarioAlta = 1 << 6,
    UsuarioModificacion = 1 << 7,
    UsuarioBaja = 1 << 8,
}
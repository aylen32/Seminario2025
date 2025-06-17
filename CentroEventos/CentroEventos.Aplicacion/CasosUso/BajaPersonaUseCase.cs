using System;

namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;

public class BajaPersonaUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IServicioAutorizacion _autorizacion;

    public BajaPersonaUseCase(
        IRepositorioPersona repositorioPersona, 
        IRepositorioReserva repositorioReserva, 
        IRepositorioEventoDeportivo repositorioEventoDeportivo, 
        IServicioAutorizacion autorizacion)
    {
        _repositorioReserva = repositorioReserva;
        _repositorioPersona = repositorioPersona;
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(int idPersona, int idUsuario)
    {
       // if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioBaja))
       //     throw new FalloAutorizacionException("No tiene permiso para eliminar personas");

        if (!_repositorioPersona.ExistePersonaPorId(idPersona))
            throw new EntidadNotFoundException($"La persona con ID {idPersona} no existe");

        var eventos = _repositorioEventoDeportivo.ObtenerTodos();
        foreach (var evento in eventos)
        {
            if (evento.ResponsableId == idPersona)
                throw new OperacionInvalidaException("No se puede eliminar la persona porque es responsable de al menos un evento");
        }

        var reservas = _repositorioReserva.ObtenerReservasPorPersona(idPersona);
        if (reservas.Any())
            throw new OperacionInvalidaException("No se puede eliminar la persona porque tiene reservas asociadas");

        _repositorioPersona.EliminarPersona(idPersona);
    }
}

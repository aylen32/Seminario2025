using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosUso;

public class ObtenerEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IServicioAutorizacion _autorizacion;

    public ObtenerEventoDeportivoUseCase(IRepositorioEventoDeportivo repo, IServicioAutorizacion autorizacion)
    {
        _repositorioEvento = repo;
        _autorizacion = autorizacion;
    }

    public EventoDeportivo? Ejecutar(int Idevento, int idUsuario)
    {
         //  if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioModificacion))
        //      throw new FalloAutorizacionException("No tiene permiso para modificar personas");
        return _repositorioEvento.ObtenerEvento(Idevento);
    }
}
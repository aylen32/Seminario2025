using System;

namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Excepciones;
public class ListadoEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly int _idUsuario;

    public ListadoEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo, IServicioAutorizacion autorizacion,
    int idUsuario)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _autorizacion = autorizacion;
        _idUsuario = idUsuario;                              
    }

    public IEnumerable<EventoDeportivo> ? Ejecutar()
    {
        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.EventoAlta)) 
        {
            throw new FalloAutorizacionException("No tiene permiso para listar eventos deportivos");
        }
        return _repositorioEventoDeportivo.ObtenerTodos();
    }
}

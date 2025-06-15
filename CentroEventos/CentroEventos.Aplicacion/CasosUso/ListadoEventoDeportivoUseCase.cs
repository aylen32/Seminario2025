using System;

namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Entidades;
public class ListadoEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IServicioAutorizacion _autorizacion;

    public ListadoEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo, IServicioAutorizacion autorizacion)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _autorizacion = autorizacion;                           
    }

    public IEnumerable<EventoDeportivo>? Ejecutar()
    {
        return _repositorioEventoDeportivo.ObtenerTodos();
    }
}

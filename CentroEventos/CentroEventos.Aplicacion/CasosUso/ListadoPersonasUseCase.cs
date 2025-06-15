using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosUso;

public class ListadoPersonasUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IServicioAutorizacion _autorizacion;
    public ListadoPersonasUseCase(IRepositorioPersona repositorioPersona, IServicioAutorizacion autorizacion)
    {
        _repositorioPersona = repositorioPersona;
        _autorizacion = autorizacion;
    }

    public IEnumerable<Persona> Ejecutar()
    {
        return _repositorioPersona.ObtenerTodas(); // Obtener la lista de personas del repositorio
    }
}

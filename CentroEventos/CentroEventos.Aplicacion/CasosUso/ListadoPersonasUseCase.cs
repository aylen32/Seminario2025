using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;
namespace CentroEventos.Aplicacion.CasosUso;

public class ListadoPersonasUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly int _idUsuario;
    public ListadoPersonasUseCase(IRepositorioPersona repositorioPersona, IServicioAutorizacion autorizacion,
    int idUsuario)
    {
        _repositorioPersona = repositorioPersona;
        _autorizacion = autorizacion;
        _idUsuario = idUsuario;
    }

    public IEnumerable<Persona> Ejecutar()
    {
        return _repositorioPersona.ObtenerTodas(); // Obtener la lista de personas del repositorio
    }
}

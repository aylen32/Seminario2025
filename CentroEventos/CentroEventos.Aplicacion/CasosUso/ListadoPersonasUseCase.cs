using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
namespace CentroEventos.Aplicacion.CasosUso;

public class ListadoPersonasUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    public ListadoPersonasUseCase(IRepositorioPersona repositorioPersona)
    {
        _repositorioPersona = repositorioPersona; 
    }

    public IEnumerable<Persona> Ejecutar()
    {
        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.UsuarioAlta))
        {
            throw new FalloAutorizacionException("No tiene permiso para listar personas");
        }
        return _repositorioPersona.ObtenerTodas(); // Obtener la lista de personas del repositorio
    }
}

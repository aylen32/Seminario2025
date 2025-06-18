using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Servicio;

namespace CentroEventos.Aplicacion.CasosUso;

public class ObtenerPersonaUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IServicioAutorizacion _autorizacion;

    public ObtenerPersonaUseCase(IRepositorioPersona repo, IServicioAutorizacion autorizacion)
    {
        _repositorioPersona = repo;
        _autorizacion = autorizacion;
    }

    public Persona? Ejecutar(int Idpersona, int idUsuario)
    {
        return _repositorioPersona.ObtenerPersona(Idpersona);
    }
}
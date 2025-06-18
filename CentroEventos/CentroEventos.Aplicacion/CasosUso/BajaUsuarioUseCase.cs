using System;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Servicio;

namespace CentroEventos.Aplicacion.CasosUso;

public class BajaUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorio;
    private readonly IServicioAutorizacion _autorizacion;

    public BajaUsuarioUseCase(IRepositorioUsuario repositorio, IServicioAutorizacion autorizacion)
    {
        _repositorio = repositorio;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(int idUsuarioAEliminar, int idSolicitante)
    {
        if (!_autorizacion.TienePermisoDeGestion(idSolicitante))
            throw new OperacionInvalidaException("No tiene permiso para eliminar usuarios.");

        _repositorio.EliminarUsuario(idUsuarioAEliminar);
    }
}
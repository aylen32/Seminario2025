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
        // Solo usuarios con los 3 permisos completos de gestión pueden eliminar usuarios
        if (!_autorizacion.TieneTodosLosPermisosGestion(idSolicitante))
            throw new OperacionInvalidaException("No tiene permiso para eliminar usuarios.");

        if (idUsuarioAEliminar == 1)
            throw new OperacionInvalidaException("No se puede eliminar al administrador principal.");

        var usuario = _repositorio.ObtenerUsuario(idUsuarioAEliminar);
        if (usuario == null)
            throw new EntidadNotFoundException("El usuario no existe.");

        _repositorio.EliminarUsuario(idUsuarioAEliminar);
    }
}
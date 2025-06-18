using System;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Servicio;


namespace CentroEventos.Aplicacion.CasosUso;

public class BajaUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IValidadorUsuario _validadorUsuario;
    private readonly IServicioAutorizacion _autorizacion;

    public BajaUsuarioUseCase(IRepositorioUsuario repositorioUsuario, IValidadorUsuario validadorUsuario,
        IServicioAutorizacion autorizacion)
    {
        _repositorioUsuario = repositorioUsuario;
        _validadorUsuario = validadorUsuario;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(int idUsuario, int idUsuarioSolicitante)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuarioSolicitante, PermisoTipo.UsuarioBaja))
          throw new FalloAutorizacionException("No tiene permiso para eliminar usuarios");


        if (!_repositorioUsuario.ExisteUsuarioPorId(idUsuario))
            throw new EntidadNotFoundException($"El usuario con ID {idUsuario} no existe");

        _repositorioUsuario.EliminarUsuario(idUsuario);
    }
    
}

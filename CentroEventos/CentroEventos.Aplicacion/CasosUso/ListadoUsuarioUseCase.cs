using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosUso;

public class ListarUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IServicioAutorizacion _autorizacion;
    public ListarUsuarioUseCase(IRepositorioUsuario repositorioUsuario, IServicioAutorizacion autorizacion)
    {
        _repositorioUsuario = repositorioUsuario;
        _autorizacion = autorizacion;
    }

    public List<Usuario>? Ejecutar()
    {
        return _repositorioUsuario.ObtenerTodos();
    }
}

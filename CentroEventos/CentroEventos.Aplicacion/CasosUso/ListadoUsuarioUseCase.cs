using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosUso;

public class ListarUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    public ListarUsuarioUseCase(IRepositorioUsuario repositorioUsuario)
    {
        _repositorioUsuario = repositorioUsuario;
    }

    public List<Usuario> Ejecutar()
    {
        return _repositorioUsuario.ObtenerTodos();
    }
}

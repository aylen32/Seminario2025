using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosUso;

public class ListadoUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    public ListadoUsuarioUseCase(IRepositorioUsuario repositorioUsuario)
    {
        _repositorioUsuario = repositorioUsuario;
    }

    public List<Usuario> Ejecutar()
    {
        return _repositorioUsuario.ObtenerTodosConPermisos();
    }
}
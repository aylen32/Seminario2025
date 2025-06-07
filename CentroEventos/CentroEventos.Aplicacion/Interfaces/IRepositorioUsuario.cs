using System;
using System.Collections.Generic;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Interfaces;

public interface IRepositorioUsuario
{
    void AgregarUsuario(Usuario usuario);
    void EliminarUsuario(int id);
    void ModificarUsuario(Usuario usuario);
    Usuario? ObtenerUsuario(int id);
    IEnumerable<Usuario> ObtenerTodos();
    bool ExisteUsuarioPorId(int id);
}

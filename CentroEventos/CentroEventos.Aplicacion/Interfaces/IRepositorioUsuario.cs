using System;

namespace CentroEventos.Aplicacion.Interfaces;

public interface IRepositorioUsuario
{
    Usuario? ObtenerPorId(int id);
    Usuario? ObtenerPorEmail(string email);
    void AgregarUsuario(Usuario usuario);
    void ModificarUsuario(Usuario usuario);
    void EliminarUsuario(int id);
    bool ExisteEmail(string email);
}
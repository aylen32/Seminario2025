using System;

namespace CentroEventos.Aplicacion.Interfaces;

using CentroEventos.Aplicacion.Entidades;

public interface IRepositorioPersona
{
    bool ExistePersonaPorId(int id);              // Verifica si una persona con el ID pasado por parametro  existe
    bool ExistePersonaPorDni(string dni, int? idExcluir = null);         
    bool ExistePersonaPorEmail(string email, int? idExcluir = null);     
    Persona ? ObtenerPersona(int id);             // Obtiene una persona con el ID
    List<Persona> ObtenerTodas();                 // Obtiene todas las personas 
    void AgregarPersona(Persona persona);         // Agrega una persona
    void ModificarPersona(Persona persona);       // Modifica una persona
    void EliminarPersona(int id);                 // Elimina una persona buscandolo por su ID
}

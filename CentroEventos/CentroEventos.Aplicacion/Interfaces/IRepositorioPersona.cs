using System;

namespace CentroEventos.Aplicacion.Validaciones;

public interface IRepositorioPersona
{
    bool ExistePersonaPorId(int id);              // Verifica si una persona con el ID pasado por parametro  existe
    bool ExistePersonaPorDni(string dni);         // Verifica si una persona con el DNI pasado por parametro existe
    bool ExistePersonaPorEmail(string email);     // Verifica si una persona con el email pasado por parametro existe
    Persona ? ObtenerPersona(int id);               // Obtiene una persona con el ID
    IEnumerable<Persona> ObtenerTodas();          // Obtiene todas las personas (Arreglo)
    void AgregarPersona(Persona persona);         // Agrega una persona
    void ModificarPersona(Persona persona);       // Modifica una persona
    void EliminarPersona(int id);                 // Elimina una persona buscandolo por su ID
}

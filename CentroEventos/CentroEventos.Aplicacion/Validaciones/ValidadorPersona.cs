using System;

namespace CentroEventos.Aplicacion.Validaciones;                      //Falta reglas del negocio

public class ValidadorPersona{           
    private readonly IRepositorioPersona _personaRepo;

    public ValidadorPersona(IRepositorioPersona personaRepo)          //Constructor para recibir el repositorio 
    {                                                                 //Creo que deberia ser publico?
        _personaRepo = personaRepo;
    }

    public void Validar(Persona persona){

      if (string.IsNullOrWhiteSpace (persona.Nombre)){
         throw new ValidacionException ("El Nombre no puede estar vacío");
      }

      if (string.IsNullOrWhiteSpace (persona.Apellido)){
        throw new ValidacionException ("El Apellido no puede estar vacío");
      }

      if (string.IsNullOrWhiteSpace (persona.Dni)){
         throw new ValidacionException ("El DNI no puede estar vacío");
      }

      if (string.IsNullOrWhiteSpace (persona.Email)){
        throw new ValidacionException ("El Email no puede estar vacío");
      }

      if (_personaRepo.ExistePersonaPorDni (persona.Dni)){
        throw new DuplicadoException ("La persona con este DNI ya está registrada");  // Verificar que el DNI no esté repetido
      }

      if (_personaRepo.ExistePersonaPorEmail (persona.Email)){                         // Verificar que el Email no esté repetido
        throw new DuplicadoException ("La persona con este Email ya está registrada");
      }
    }
}

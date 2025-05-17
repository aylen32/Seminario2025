using System;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.Validaciones;                      

public class ValidadorPersona: IValidadorPersona{           
    private readonly IRepositorioPersona _personaRepo;

    public ValidadorPersona(IRepositorioPersona personaRepo)          
    {                                                                 
        _personaRepo = personaRepo;
    }

    public void Validar(Persona persona){

      if (string.IsNullOrWhiteSpace (persona.Nombre)){
         throw new ValidacionException ("El Nombre no puede estar vacío");
      }

      if (string.IsNullOrWhiteSpace (persona.Apellido)){
        throw new ValidacionException ("El Apellido no puede estar vacío");
      }

      if (string.IsNullOrWhiteSpace (persona.DNI)){
         throw new ValidacionException ("El DNI no puede estar vacío");
      }

      if (string.IsNullOrWhiteSpace (persona.Mail)){
        throw new ValidacionException ("El Email no puede estar vacío");
      }

      if (_personaRepo.ExistePersonaPorDni (persona.DNI)){
        throw new DuplicadoException ("La persona con este DNI ya está registrada");  
      }

      if (_personaRepo.ExistePersonaPorEmail (persona.Mail)){                        
        throw new DuplicadoException ("La persona con este Email ya está registrada");
      }
    }
}

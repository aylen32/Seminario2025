using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;

namespace CentroEventos.Aplicacion.CasosUso;

public class AltaPersonaUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IValidadorPersona _validadorPersona;

    public AltaPersonaUseCase(IRepositorioPersona repositorioPersona, IValidadorPersona validadorPersona){

        _repositorioPersona = repositorioPersona;     //Inyeccion de dependencia por constructor (supuestamente se puede)
        _validadorPersona = validadorPersona;         //No se si esto estaria bien! ¡Consultar!
    }

    public void Ejecutar(Persona persona){

        _validadorPersona.Validar(persona);           // Validar la persona antes de guardarla
        _repositorioPersona.AgregarPersona(persona);  // Si pasa la validación, agregarla al repositorio
    }
}

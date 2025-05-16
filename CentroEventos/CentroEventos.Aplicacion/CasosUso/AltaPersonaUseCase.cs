using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;

namespace CentroEventos.Aplicacion.CasosUso;

public class AltaPersonaUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IValidadorPersona _validadorPersona;

    public AltaPersonaUseCase(IRepositorioPersona repositorioPersona, IValidadorPersona validadorPersona){

        _repositorioPersona = repositorioPersona;     //Inyeccion de dependencia por constructor (supuestamente se puede)
        _validadorPersona = validadorPersona;         
    }

    public void Ejecutar(Persona persona) {

        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.UsuarioAlta))
        {
            throw new FalloAutorizacionException ("No tiene permiso para dar de alta personas");
        }

        _validadorPersona.Validar(persona);           // Validar la persona antes de guardarla
        _repositorioPersona.AgregarPersona(persona);  // Si pasa la validación, agregarla al repositorio
    }
}

using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Servicio;

namespace CentroEventos.Aplicacion.CasosUso;
    public class AltaPersonaUseCase
    {
      private readonly IRepositorioPersona _repositorioPersona;
      private readonly IValidadorPersona _validadorPersona;
      private readonly IServicioAutorizacion _autorizacion;
      private readonly int _idUsuario;

      public AltaPersonaUseCase(IRepositorioPersona repositorioPersona, IValidadorPersona validadorPersona,
        IServicioAutorizacion autorizacion, int idUsuario)
      {
        _repositorioPersona = repositorioPersona;
        _validadorPersona = validadorPersona;
        _autorizacion = autorizacion;
        _idUsuario = idUsuario;
      }

      public void Ejecutar(Persona persona)
      {
        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.UsuarioAlta))
        {
            throw new FalloAutorizacionException("No tiene permiso para dar de alta personas");
        }

        if (!_validadorPersona.Validar(persona))
        {
            throw new ValidacionException(_validadorPersona.ObtenerError() ?? "Error desconocido en la validación");
        }

        _repositorioPersona.AgregarPersona(persona);
      }
    }
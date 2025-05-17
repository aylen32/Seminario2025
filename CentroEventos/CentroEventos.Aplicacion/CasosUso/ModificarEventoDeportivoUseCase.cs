using System;

namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Enumerativos;


public class ModificarEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IValidadorEventoDeportivo _validadorEventoDeportivo;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly int _idUsuario;

    public ModificarEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo, IValidadorEventoDeportivo validadorEventoDeportivo, IServicioAutorizacion autorizacion,
    int idUsuario)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _validadorEventoDeportivo = validadorEventoDeportivo;
        _autorizacion = autorizacion;
        _idUsuario = idUsuario;
    }

    public void Ejecutar(EventoDeportivo evento)
    {
        if (!_autorizacion.PoseeElPermiso(_idUsuario, Permiso.EventoModificacion))
        {
            throw new FalloAutorizacionException("No tiene permiso para modificar eventos");
        }
        if (!_repositorioEventoDeportivo.ExisteEventoPorId(evento.Id))
            {
                throw new EntidadNotFoundException($"El evento deportivo con ID {evento.Id} no existe");
            }

        _validadorEventoDeportivo.Validar(evento);
        _repositorioEventoDeportivo.ModificarEvento(evento);
    }
}
using System;

namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;

public class ModificarEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IValidadorEventoDeportivo _validadorEventoDeportivo;

    public ModificarEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo, IValidadorEventoDeportivo validadorEventoDeportivo)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _validadorEventoDeportivo = validadorEventoDeportivo;
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

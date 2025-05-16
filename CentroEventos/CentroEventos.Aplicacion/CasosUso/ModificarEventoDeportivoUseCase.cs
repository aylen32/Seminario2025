using System;

namespace CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;

public class ModificarEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;

    public ModificarEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
    }

    public void Ejecutar(EventoDeportivo evento)
    {
        if (!_repositorioEventoDeportivo.ExisteEventoPorId(evento.Id))
        {
            throw new EntidadNoEncontradaException($"El evento deportivo con ID {evento.Id} no existe.");
        }

        _repositorioEventoDeportivo.ModificarEvento(evento);
    }
}

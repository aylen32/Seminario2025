using System;

namespace CentroEventos.Aplicacion.Interfaces;

using CentroEventos.Aplicacion.Entidades;

public interface IValidadorEventoDeportivo
{
    bool Validar(EventoDeportivo evento);
    string? ObtenerError();
}
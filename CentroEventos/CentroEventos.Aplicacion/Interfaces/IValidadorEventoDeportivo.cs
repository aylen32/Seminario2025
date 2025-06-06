using System;

namespace CentroEventos.Aplicacion.Interfaces;

public interface IValidadorEventoDeportivo
{
    bool Validar(EventoDeportivo evento);
    string? ObtenerError();
}


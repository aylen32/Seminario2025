using System;

namespace CentroEventos.Aplicacion.Interfaces;

using CentroEventos.Aplicacion.Entidades;

public interface IValidadorReserva
{
    bool ValidarParaAlta(Reserva reserva);
    bool ValidarParaModificacion(Reserva reserva);
    string? ObtenerError();
}
using System;

namespace CentroEventos.Aplicacion.Interfaces;

using CentroEventos.Aplicacion.Entidades;

public interface IValidadorReserva
{
    bool Validar(Reserva reserva);
    string? ObtenerError();
}

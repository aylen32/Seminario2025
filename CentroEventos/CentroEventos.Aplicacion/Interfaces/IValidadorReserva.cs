using System;

namespace CentroEventos.Aplicacion.Interfaces;

public interface IValidadorReserva
{
    bool Validar(Reserva reserva);
    string? ObtenerError();
}

using System;

namespace CentroEventos.Aplicacion.CasosUso;

using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;

public class ModificarReservaUseCase
{
    private readonly IRepositorioReserva _repo;
    private readonly IValidadorReserva _validador;

    public ModificarReservaUseCase(IRepositorioReserva repo, IValidadorReserva validador)
    {
        _repo = repo;
        _validador = validador;
    }

    public void Ejecutar(Reserva reserva)
    {
        if (!_repo.ExisteReservaPorId(reserva.Id))
        {
            throw new EntidadNoEncontradaException($"La reserva con ID {reserva.Id} no existe.");
        }

        _validador.Validar(reserva);
        _repo.ModificarReserva(reserva);

    }
}
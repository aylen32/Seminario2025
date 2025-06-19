using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.CasosUso;

public class ObtenerReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;

    public ObtenerReservaUseCase(IRepositorioReserva repo)
    {
        _repositorioReserva = repo;
    }

    public Reserva? Ejecutar(int Idreserva)
    {
        return _repositorioReserva.ObtenerReserva(Idreserva);
    }
}
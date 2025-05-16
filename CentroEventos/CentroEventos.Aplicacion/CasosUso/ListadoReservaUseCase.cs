using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
namespace CentroEventos.Aplicacion.CasosUso;

public class ListadoReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    public ListadoReservaUseCase(IRepositorioReserva repositorioReserva)
    {
        _repositorioReserva = repositorioReserva; // Inyeccion de dependencia por constructor (supuestamente se puede)
    }

    public IEnumerable<Reserva> ? Ejecutar()
    {
        return null; // Obtener la lista de reservas del repositorio
    }
    
}

using System;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
namespace CentroEventos.Aplicacion.CasosUso;

public class BajaReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
   
    public BajaReservaUseCase(IRepositorioReserva repositorioReserva)
    {
        _repositorioReserva = repositorioReserva;
      
    }
    public void Ejecutar(int id)
    { 
        _repositorioReserva.EliminarReserva(id);
    }

}

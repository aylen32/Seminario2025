using System;
using CentroEventos.Aplicacion;
using CentroEventos.Aplicacion.Validaciones;

namespace CentroEventos.Repositorios;


public class RepositorioReserva : IRepositorioReserva
{
    private readonly string _archivo = @"C:\Users\aylen\OneDrive\Documentos\proyecto2025\archivo_reserva.txt";
    private readonly string _archivo_id = @"C:\Users\aylen\OneDrive\Documentos\proyecto2025\archivo_id_reserva.txt";
     private int buscarUltID(){
       int id;
       using var reader = new StreamReader(_archivo_id);
       int ultId = int.Parse(reader.ReadToEnd());
       id=ultId++;
       using var writer = new StreamWriter(_archivo_id, false);
            {
                writer.Write(id);
            }
        return id;
    }
    private int GenerarId() {
        int idNuevo = buscarUltID();
        return idNuevo;    
        
    }
    private string cadenaReserva(Reserva reserva){
        return reserva.Id +","+reserva.PersonaId+","+reserva.EventoDeportivoId+","+reserva.FechaAltaReserva;
    }
    public void AgregarReserva(Reserva reserva)
    {
        int idReserva = GenerarId();
        using var sw = new StreamWriter(_archivo, true);
        sw.WriteLine(cadenaReserva(reserva));
    }

    public void EliminarReserva(int id)
    {
        throw new NotImplementedException();
    }

    public bool ExisteReservaDuplicada(int personaId, int eventoId)
    {
        throw new NotImplementedException();
    }

    public bool ExisteReservaPorId(int id)
    {
        throw new NotImplementedException();
    }

    public void ModificarReserva(Reserva reserva)
    {
        throw new NotImplementedException();
    }

    public Reserva ObtenerReserva(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Reserva> ObtenerReservasPorEvento(int eventoId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Reserva> ObtenerReservasPorPersona(int personaId)
    {
        throw new NotImplementedException();
    }
}

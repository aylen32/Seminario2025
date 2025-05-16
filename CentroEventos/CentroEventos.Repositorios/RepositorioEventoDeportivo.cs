using System;
using CentroEventos.Aplicacion;
using CentroEventos.Aplicacion.Validaciones;

namespace CentroEventos.Repositorios;

public class RepositorioEventoDeportivo : IRepositorioEventoDeportivo
{
     private readonly string _archivo = @"C:\Users\aylen\OneDrive\Documentos\proyecto2025\archivo_evento.txt";
    private readonly string _archivo_id = @"C:\Users\aylen\OneDrive\Documentos\proyecto2025\archivo_id_evento.txt";

    private int buscarUltID(){
       int id;
       using var reader = new StreamReader(_archivo_id);
       int ultId = int.Parse(reader.ReadToEnd());
       id=ultId+1;
       using var writer = new StreamWriter(_archivo_id, false);
            {
                writer.Write(id);
            }
        return id;
    }
    private int GenerarId()
    {
        int idNuevo = buscarUltID();
        return idNuevo;
    }
    public void AgregarEvento(EventoDeportivo evento)
    {
        evento.Id = GenerarId();
        using var sw = new StreamWriter(_archivo, true);
        sw.WriteLine(cadenaEvento(evento));
    }
    private string cadenaEvento(EventoDeportivo evento){
        return evento.Id+","+evento.Nombre+","+evento.Descripcion+","+evento.FechaHoraInicio+","+evento.DuracionHoras+","+evento.CupoMaximo+","+evento.ResponsableId;
    }
    public void EliminarEvento(int id)
    {
        throw new NotImplementedException();
    }

    public bool ExisteEventoPorId(int id)
    {
        throw new NotImplementedException();
    }

    public void ModificarEvento(EventoDeportivo evento)
    {
        throw new NotImplementedException();
    }

    public EventoDeportivo ObtenerEvento(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<EventoDeportivo> ObtenerTodos()
    {
        throw new NotImplementedException();
    }
}

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
        using var reader = new StreamReader(_archivo);
        using var writer = new StreamWriter(_archivo,false);
                List<string> nuevasLineas = new List<string>();        
        if(!ExisteEventoPorId(id)){
            throw new KeyNotFoundException($"ID {id} no encontrado");
        }else{
            string linea;
            while((linea=reader.ReadLine())!=null){

                EventoDeportivo e = convertirString(linea);
                if(e.Id!=id){
                    nuevasLineas.Add(linea);
                }
            }
            foreach (string l in nuevasLineas)
            {
                writer.WriteLine(l);
            }
        }
    }
    private static EventoDeportivo convertirString(string e)
    {
        string[] partes = e.Split(",");
        EventoDeportivo evento = new EventoDeportivo();
        evento.Id = int.Parse(partes[0]);
        evento.Nombre = partes[1];
        evento.Descripcion = partes[2];
        evento.FechaHoraInicio = DateTime.Parse(partes[3]);
        evento.DuracionHoras = int.Parse(partes[4]);
        evento.CupoMaximo = int.Parse(partes[5]);
        evento.ResponsableId = int.Parse(partes[6]);
        return evento;
    }

    public bool ExisteEventoPorId(int id)
    {
        using var reader = new StreamReader(_archivo);
        string linea;
        while ((linea = reader.ReadLine()) != null)
        {
            EventoDeportivo evento = convertirString(linea);
            if (evento.Id == id)
            {
                return true;
            }
        }
        return false;
    }

    public void ModificarEvento(EventoDeportivo evento)
    {
        throw new NotImplementedException();
    }

    public EventoDeportivo ObtenerEvento(int id)
    {
        using var reader = new StreamReader(_archivo);
        string linea;
        while ((linea = reader.ReadLine()) != null)
        {
            EventoDeportivo evento = convertirString(linea);
            if (evento.Id == id)
            {
                return evento;
            }
        }
        return null;
    }

    public IEnumerable<EventoDeportivo> ObtenerTodos()
    {
        List<EventoDeportivo> eventos = new List<EventoDeportivo>();
        using var reader = new StreamReader(_archivo);
        string linea;
        while ((linea = reader.ReadLine()) != null)
        {
            EventoDeportivo e = convertirString(linea);
            eventos.Add(e);
        }
        return eventos;
    }
}

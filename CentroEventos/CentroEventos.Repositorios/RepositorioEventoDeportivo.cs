using System;
using CentroEventos.Aplicacion;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
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
        if (!ExisteEventoPorId(id))
        {
            throw new EntidadNoEncontradaException($"El evento con ID {id} no existe.");
        }
        var nuevasLineas = new List<string>();
        using (var reader = new StreamReader(_archivo))
        {
          string ? linea;
          while ((linea = reader.ReadLine()) != null)
          {
            var e = convertirString(linea);
            if (e.Id != id)
                nuevasLineas.Add(linea);
          }
        }
        using (var writer = new StreamWriter(_archivo, false))
        {
          foreach (var l in nuevasLineas)
            writer.WriteLine(l);
        }
    }

    private EventoDeportivo convertirString(string linea)
    {
        var partes = linea.Split(',');
        return new EventoDeportivo
        {
            Id = int.Parse(partes[0]),
            Nombre = partes[1],
            Descripcion = partes[2],
            FechaHoraInicio = DateTime.Parse(partes[3]),
            DuracionHoras = double.Parse(partes[4]),
            CupoMaximo = int.Parse(partes[5]),
            ResponsableId = int.Parse(partes[6])
        };
    }

    public bool ExisteEventoPorId(int id)
    {
      using var reader = new StreamReader(_archivo);
      string ? linea;
      while ((linea = reader.ReadLine()) != null)
      {
        var e = convertirString(linea);
        if (e.Id == id)
            return true;
      }
      return false;
    }

    public void ModificarEvento(EventoDeportivo evento)
    {
        if (!ExisteEventoPorId(evento.Id))
        {
            throw new EntidadNoEncontradaException($"El evento con ID {evento.Id} no existe.");
        }
        var nuevasLineas = new List<string>();
        using (var reader = new StreamReader(_archivo))
        {
            string ? linea;
            while ((linea = reader.ReadLine()) != null)
            {
                var e = convertirString(linea);
                if (e.Id == evento.Id)
                    nuevasLineas.Add(cadenaEvento(evento));
                else
                    nuevasLineas.Add(linea);
            }
        }
        using (var writer = new StreamWriter(_archivo, false))
        {
            foreach (var l in nuevasLineas)
                writer.WriteLine(l);
        }
    }

    public EventoDeportivo ? ObtenerEvento(int id)
    {
      using var reader = new StreamReader(_archivo);
      string? linea;
      while ((linea = reader.ReadLine()) != null)
      {
        var e = convertirString(linea);
        if (e.Id == id)
            return e;
      }
      return null;
    }

    public IEnumerable<EventoDeportivo> ObtenerTodos()
    {
      var lista = new List<EventoDeportivo>();
      using var reader = new StreamReader(_archivo);
      string ?linea;
      while ((linea = reader.ReadLine()) != null)
      {
        if (!string.IsNullOrWhiteSpace(linea))
            lista.Add(convertirString(linea));
      }
      return lista;
    }
}
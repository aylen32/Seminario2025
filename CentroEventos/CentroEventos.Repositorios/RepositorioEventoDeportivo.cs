using System;
using CentroEventos.Aplicacion;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Repositorios;

public class RepositorioEventoDeportivo : IRepositorioEventoDeportivo
{
     private readonly string _archivo = @"C:\Users\aylen\datos\archivo_evento.txt";
    private readonly string _archivo_id = @"C:\Users\aylen\datos\archivo_id_evento.txt";

       private int buscarUltID()
    {
        int id;
        string? ultId;
        using (var reader = new StreamReader(_archivo_id))
        {
            ultId = reader.ReadToEnd();
        }

        if (string.IsNullOrWhiteSpace(ultId))
            ultId = "0";

        id = int.Parse(ultId) + 1;
        using (var writer = new StreamWriter(_archivo_id, false))
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
    private string cadenaEvento(EventoDeportivo evento)
    {
        return evento.Id+","+evento.Nombre+","+evento.Descripcion+","+evento.FechaHoraInicio+","+evento.DuracionHoras+","+evento.CupoMaximo+","+evento.ResponsableId;
    }
    
    public void EliminarEvento(int id)
    {
       List<string> nuevasLineas = new List<string>();

    if (!ExisteEventoPorId(id))
        throw new EntidadNotFoundException($"El evento con ID {id} no existe");

    //  leer
    using (var reader = new StreamReader(_archivo))
    {
        string? linea;
        while ((linea = reader.ReadLine()) != null)
        {
            EventoDeportivo e = convertirString(linea);
            if (e.Id != id)
            {
                nuevasLineas.Add(linea); // solo guardás las que no son la que querés eliminar
            }
        }
    }

    //  escribir
    using (var writer = new StreamWriter(_archivo, false))
    {
        foreach (string l in nuevasLineas)
        {
            writer.WriteLine(l);
        }
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

    public void ModificarEvento(int id, string n, string d, DateTime f, double dur, int cupo)
    {
        


        if (!ExisteEventoPorId(id))
        {
            throw new EntidadNotFoundException($"El evento con ID {id} no existe.");
        }
        var nuevasLineas = new List<string>();
        using (var reader = new StreamReader(_archivo))
        {
            string ? linea;
            while ((linea = reader.ReadLine()) != null)
            {
                var e = convertirString(linea);
                if (e.Id == id) {
                    e.Nombre = n;
                    e.Descripcion = d;
                    e.FechaHoraInicio = f;
                    e.DuracionHoras = dur;
                    e.CupoMaximo = cupo;
                    

                    nuevasLineas.Add(cadenaEvento(e));
                }
                    
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
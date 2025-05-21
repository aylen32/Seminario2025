using System;
using CentroEventos.Aplicacion;
using CentroEventos.Aplicacion.Validaciones;
using static CentroEventos.Aplicacion.Reserva;
using CentroEventos.Aplicacion.Excepciones;
using System.Data.Common;
namespace CentroEventos.Repositorios;


public class RepositorioReserva : IRepositorioReserva
{
    private readonly string _archivo = @"C:\Users\aylen\datos\archivo_reserva.txt";
    private readonly string _archivo_id = @"C:\Users\aylen\datos\archivo_id_reserva.txt";
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
    private string cadenaReserva(Reserva reserva)
    {
        return reserva.Id + "," + reserva.PersonaId + "," + reserva.EventoDeportivoId + "," + reserva.FechaAltaReserva + "," + reserva.Estado;
    }
    public void AgregarReserva(Reserva reserva)
    {
        int idReserva = GenerarId();
        reserva.Id = idReserva;
        using var sw = new StreamWriter(_archivo, true);
        sw.WriteLine(cadenaReserva(reserva));
    }
    private static Reserva convertirString(string r)
    {
        string[] partes = r.Split(",");
        Reserva re = new Reserva();
        re.Id = int.Parse(partes[0]);
        re.PersonaId = int.Parse(partes[1]);
        re.EventoDeportivoId = int.Parse(partes[2]);
        re.FechaAltaReserva = DateTime.Parse(partes[3]);
        re.Estado = (EstadoAsistencia)Enum.Parse(typeof(EstadoAsistencia), partes[4]);
        return re;
    }

    public void EliminarReserva(int id)
    {
        List<string> nuevasLineas = new List<string>();

    if (!ExisteReservaPorId(id))
        throw new EntidadNotFoundException($"La reserva con ID {id} no existe");

    //  leer
    using (var reader = new StreamReader(_archivo))
    {
        string? linea;
        while ((linea = reader.ReadLine()) != null)
        {
            Reserva r = convertirString(linea);
            if (r.Id != id)
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

    public bool ExisteReservaDuplicada(int personaId, int eventoId)
    {

        using var reader = new StreamReader(_archivo);
        string? linea;
        while ((linea = reader.ReadLine()) != null)
        {
            Reserva r = convertirString(linea);
            if (r.PersonaId == personaId && r.EventoDeportivoId == eventoId)
            {
                return true;
            }
        }
        return false;
    }

    public bool ExisteReservaPorId(int id)
    {
        using var reader = new StreamReader(_archivo);
        string? linea;
        while ((linea = reader.ReadLine()) != null)
        {
            Reserva r = convertirString(linea);
            if (r.Id == id)
            {
                return true;
            }
        }
        return false;
    }

    public void ModificarReserva(int id, EstadoAsistencia estado)
    {
        if (!ExisteReservaPorId(id))
        {
            throw new EntidadNotFoundException($"La reserva con ID {id} no existe");
        }
        var nuevasLineas = new List<string>();
        using (var reader = new StreamReader(_archivo))
        {
            string? linea;
            while ((linea = reader.ReadLine()) != null)
            {
                Reserva r = convertirString(linea);
                if (r.Id == id)
                {
                    r.Estado = estado;
                    nuevasLineas.Add(cadenaReserva(r));
                }

                else
                {
                    nuevasLineas.Add(linea);
                }
                     
            }
        }

        using (var writer = new StreamWriter(_archivo, false))
        {
            foreach (var l in nuevasLineas)
                writer.WriteLine(l);
        }
    }
    

    public Reserva? ObtenerReserva(int id)
    {
        using var reader = new StreamReader(_archivo);
        string? unaReserva;
        while ((unaReserva = reader.ReadLine()) != null)
        {
            Reserva r = convertirString(unaReserva);
            if (r.Id == id)
            {
                return r;
            }
        }
        return null;
    }

    public IEnumerable<Reserva> ObtenerReservasPorEvento(int eventoId)
    {
        var reservas = new List<Reserva>();
        using var reader = new StreamReader(_archivo);
        string? linea;
        while ((linea = reader.ReadLine()) != null)
        {
            var r = convertirString(linea);
            if (r.EventoDeportivoId == eventoId)
                reservas.Add(r);
        }
        return reservas;
    }

    public IEnumerable<Reserva> ObtenerReservasPorPersona(int personaId)
    {
        var reservas = new List<Reserva>();
        using var reader = new StreamReader(_archivo);
        string? linea;
        while ((linea = reader.ReadLine()) != null)
        {
            var r = convertirString(linea);
            if (r.PersonaId == personaId)
                reservas.Add(r);
        }
        return reservas;
    }
    public IEnumerable<Reserva> ObtenerTodas()
    {
        var lista = new List<Reserva>();
        using var reader = new StreamReader(_archivo);
        string? linea;
        while ((linea = reader.ReadLine()) != null)
        {
            Reserva r = convertirString(linea);
            lista.Add(r);
        }
        return lista;
    }
}
using System;
using CentroEventos.Aplicacion;
using CentroEventos.Aplicacion.Validaciones;
using static CentroEventos.Aplicacion.Reserva;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Repositorios;


public class RepositorioReserva : IRepositorioReserva
{
    private readonly string _archivo = @"C:\Users\aylen\OneDrive\Documentos\proyecto2025\archivo_reserva.txt";
    private readonly string _archivo_id = @"C:\Users\aylen\OneDrive\Documentos\proyecto2025\archivo_id_reserva.txt";
    private int buscarUltID()
    {
        int id;
        using var reader = new StreamReader(_archivo_id);
        int ultId = int.Parse(reader.ReadToEnd());
        id = ultId + 1;
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
        using var reader = new StreamReader(_archivo);
        using var writer = new StreamWriter(_archivo, false);
        List<string> nuevasLineas = new List<string>();
        if (!ExisteReservaPorId(id))
        {
            throw new EntidadNoEncontradaException($"La reserva de persona con ID {id} no existe.");
        }
        else
        {
            string? linea;
            while ((linea = reader.ReadLine()) != null)
            {

                Reserva r = convertirString(linea);
                if (r.Id != id)
                {
                    nuevasLineas.Add(linea);
                }
            }
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

    public void ModificarReserva(Reserva reserva)
    {
        if (!ExisteReservaPorId(reserva.Id))
        {
            throw new EntidadNoEncontradaException($"La reserva de la persona con ID {reserva.Id} no existe.");
        }
        var nuevasLineas = new List<string>();
        using (var reader = new StreamReader(_archivo))
        {
            string? linea;
            while ((linea = reader.ReadLine()) != null)
            {
                var r = convertirString(linea);
                if (r.Id == reserva.Id)
                    nuevasLineas.Add(cadenaReserva(reserva)); // reemplaza
                else
                    nuevasLineas.Add(linea); // mantiene
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
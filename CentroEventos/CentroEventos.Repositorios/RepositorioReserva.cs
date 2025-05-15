using System;
using CentroEventos.Aplicacion;
using CentroEventos.Aplicacion.CasosUso;
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
       id=++ultId;
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
        return reserva.Id +","+reserva.PersonaId+","+reserva.EventoDeportivoId+","+reserva.FechaAltaReserva+","+reserva.Estado;
    }
    public void AgregarReserva(Reserva reserva)
    {
        int idReserva = GenerarId();
        using var sw = new StreamWriter(_archivo, true);
        sw.WriteLine(cadenaReserva(reserva));
    }
      private static Reserva convertirString(string r)
        {
        string[] partes = r.Split(",");
        Reserva re = new Reserva();
        re.Id= int.Parse(partes[0]);
        re.PersonaId= int.Parse(partes[1]);
        re.EventoDeportivoId=int.Parse(partes[2]);
        re.FechaAltaReserva=DateTime.Parse(partes[3]);
        //re.Estado=(partes[4]); Averiguar cómo parsear un Enum!!
        return re;
        }

    public void EliminarReserva(int id)
    {
        using var reader = new StreamReader(_archivo);
        using var writer = new StreamWriter(_archivo,false);
                List<string> nuevasLineas = new List<string>();        
        if(!ExisteReservaPorId(id)){
            throw new KeyNotFoundException($"ID {id} no encontrado");
        }else{
            string linea;
            while((linea=reader.ReadLine())!=null){

                Reserva r = convertirString(linea);
                if(r.Id!=id){
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
        throw new NotImplementedException();
    }

    public bool ExisteReservaPorId(int id)
    {
        using var reader = new StreamReader(_archivo);
        string linea;
        while((linea = reader.ReadLine())!=null){
            Reserva r = convertirString(linea);
            if(r.Id==id){
                return true;
            }
        }
        return false;
    }

    public void ModificarReserva(Reserva reserva)
    {
        throw new NotImplementedException();
    }

    public Reserva ObtenerReserva(int id)
    {
        using var reader = new StreamReader(_archivo);
        string? unaReserva;
        while((unaReserva= reader.ReadLine())!=null){
            Reserva r = convertirString(unaReserva);
            if(r.Id==id){
                return r;
            }
        }
        return null;
    }

    public IEnumerable<Reserva> ObtenerReservasPorEvento(int eventoId)
    {
        List<Reserva> reservas = new List<Reserva>();
       using var reader = new StreamReader(_archivo);
       string? unaReserva;
       while((unaReserva=reader.ReadLine())!=null){
            if(!string.IsNullOrWhiteSpace(unaReserva)){
               Reserva r = convertirString(unaReserva);
               if(r.EventoDeportivoId==eventoId){
                    reservas.Add(r);
               }
            }
       }
       return reservas;
    }

    public IEnumerable<Reserva> ObtenerReservasPorPersona(int personaId)
    {
        List<Reserva> reservas = new List<Reserva>();
       using var reader = new StreamReader(_archivo);
       string? unaReserva;
       while((unaReserva=reader.ReadLine())!=null){
            if(!string.IsNullOrWhiteSpace(unaReserva)){
               Reserva r = convertirString(unaReserva);
               if(r.EventoDeportivoId==personaId){
                    reservas.Add(r);
               }
            }
       }
       return reservas;      
    }
}

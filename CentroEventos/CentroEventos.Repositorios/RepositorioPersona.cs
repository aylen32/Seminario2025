using System;

using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel;
using System.Data.Common;

namespace CentroEventos.Repositorios;

public class RepositorioPersona : IRepositorioPersona
{
    private readonly string _archivo = @"C:\Users\aylen\OneDrive\Documentos\proyecto2025\archivo.txt";
    private readonly string _archivo_id = @"C:\Users\aylen\OneDrive\Documentos\proyecto2025\archivo_id.txt";
    // Elimina una persona buscandolo por su ID
    public void AgregarPersona(Persona persona)
    {
        persona.Id=GenerarId();
        using var sw = new StreamWriter(_archivo, true);
        sw.WriteLine(cadenaPersona(persona));
        
    }
    private int buscarUltID(){
       int id;
       using var reader = new StreamReader(_archivo_id);
       int ultId = int.Parse(reader.ReadToEnd());
       id= ultId + 1;
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
    private string cadenaPersona(Persona persona){
        return persona.Id+","+persona.DNI+","+persona.Nombre+","+persona.Apellido+","+persona.Mail+","+persona.Telefono;
    }
    public void EliminarPersona(int id)
    {
        using var reader = new StreamReader(_archivo);
        using var writer = new StreamWriter(_archivo,false);
                List<string> nuevasLineas = new List<string>();        
        if(!ExistePersonaPorId(id)){
            throw new EntidadNoEncontradaException($"La persona con ID {id} no existe.");
        }else{
            string ? linea;
            while((linea=reader.ReadLine())!=null){

                Persona p = convertirString(linea);
                if(p.Id!=id){
                    nuevasLineas.Add(linea);
                }
            }
            foreach (string l in nuevasLineas)
            {
                writer.WriteLine(l);
            }
        }

    }

    private Persona convertirString(string p){
        string[] partes = p.Split(",");
        Persona per = new Persona();
        per.Id= int.Parse(partes[0]);
        per.DNI= partes[1];
        per.Nombre=partes[2];
        per.Apellido=partes[3];
        per.Mail=partes[4];
        per.Telefono=partes[5];
        return per;
    }
    public bool ExistePersonaPorDni(string dni)
    {
        using var reader = new StreamReader(_archivo);
        string ? linea;
        while((linea = reader.ReadLine())!=null){
            Persona per = convertirString(linea);
            if(per.DNI.Equals(dni)){
                return true;
            }
        }
        return false;
    }

    public bool ExistePersonaPorEmail(string mail)
    {
        using var reader = new StreamReader(_archivo);
        string ? linea;
        while((linea = reader.ReadLine())!=null){
            Persona per = convertirString(linea);
            if(per.Mail.Equals(mail)){
                return true;
            }
        }
        return false;   
    }

    public bool ExistePersonaPorId(int id)
    {
        using var reader = new StreamReader(_archivo);
        string ? linea;
        while((linea = reader.ReadLine())!=null){
            Persona per = convertirString(linea);
            if(per.Id==id){
                return true;
            }
        }
        return false;
    }

    public void ModificarPersona(Persona persona)
    {
      if (!ExistePersonaPorId(persona.Id)) { 
        throw new EntidadNoEncontradaException($"La persona con ID {id} no existe.");  //Validar si existe la persona con el Id
      }
      var nuevasLineas = new List<string>();
      using (var reader = new StreamReader(_archivo))
      {
        string ? linea;
        while ((linea = reader.ReadLine()) != null)
        {
            Persona p = convertirString(linea);
            if (p.Id == persona.Id)
                nuevasLineas.Add(cadenaPersona(persona));    // actualiza los datos
            else
                nuevasLineas.Add(linea);                    // mantiene los datos
        }
      }
      using (var writer = new StreamWriter(_archivo, false))
      {
        foreach (string l in nuevasLineas)                       //Abre archivo y modifica datos viejos con nuevos
        {
            writer.WriteLine(l);
        }
      }
    }

    public Persona ? ObtenerPersona(int id)
    {
        using var reader = new StreamReader(_archivo);
        string? unaPersona;
        while((unaPersona= reader.ReadLine())!=null){
            Persona p = convertirString(unaPersona);
            if(p.Id==id){
                return p;
            }
        }
        return null;
    }

    public IEnumerable<Persona> ObtenerTodas()
    {
       List<Persona> personas = new List<Persona>();
       using var reader = new StreamReader(_archivo);
       string? unaPersona;
       while((unaPersona=reader.ReadLine())!=null){
            if(!string.IsNullOrWhiteSpace(unaPersona)){
                personas.Add(convertirString(unaPersona));
            }
       }
       return personas;
    }
}
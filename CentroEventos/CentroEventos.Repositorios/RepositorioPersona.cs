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
    private string cadenaPersona(Persona persona){
        return persona.Id+","+persona.DNI+","+persona.Nombre+","+persona.Apellido+","+persona.Mail+","+persona.Telefono;
    }
    public void EliminarPersona(int id)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public bool ExistePersonaPorEmail(string email)
    {
        throw new NotImplementedException();
    }

    public bool ExistePersonaPorId(int id)
    {
        throw new NotImplementedException();
    }

    public void ModificarPersona(Persona persona)
    {
        throw new NotImplementedException();
    }

    public Persona? ObtenerPersona(int id)
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


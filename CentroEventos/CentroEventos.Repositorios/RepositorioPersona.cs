using System;
using System.Collections.Generic;
using System.Linq;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Repositorios
{
    public class RepositorioPersona : IRepositorioPersona
    {
        private readonly CentroEventosContext _context;

        public RepositorioPersona(CentroEventosContext context)
        {
            _context = context;
        }

        public void AgregarPersona(Persona persona)
        {
            _context.Personas.Add(persona);
            _context.SaveChanges();
        }

        public void EliminarPersona(int id)
        {
            var persona = _context.Personas.Find(id);
            if (persona == null)
                throw new EntidadNotFoundException($"La persona con ID {id} no existe");

            _context.Personas.Remove(persona);
            _context.SaveChanges();
        }

        public void ModificarPersona(Persona persona)
        {
            var existente = _context.Personas.Find(persona.Id);
            if (existente == null)
                throw new EntidadNotFoundException($"La persona con ID {persona.Id} no existe.");

            existente.Nombre = persona.Nombre;
            existente.Apellido = persona.Apellido;
            existente.DNI = persona.DNI;
            existente.Mail = persona.Mail;
            existente.Telefono = persona.Telefono;

            _context.SaveChanges();
        }

        public Persona? ObtenerPersona(int id)
        {
            return _context.Personas.Find(id);
        }

        public List<Persona> ObtenerTodas()
        {
            return _context.Personas.ToList();
        }

        public bool ExistePersonaPorId(int id)
        {
            return _context.Personas.Any(p => p.Id == id);
        }

        public bool ExistePersonaPorDni(string dni, int? idExcluir = null)
        {
          var query = _context.Personas.Where(p => p.DNI == dni);
          if (idExcluir.HasValue)
          {
            query = query.Where(p => p.Id != idExcluir.Value);
          }
          return query.Any();
        }

        public bool ExistePersonaPorEmail(string email, int? idExcluir = null)
        {
           var query = _context.Personas.Where(p => p.Mail == email);
           if (idExcluir.HasValue)
           {
             query = query.Where(p => p.Id != idExcluir.Value);
           }
           return query.Any();
        }
    }
}
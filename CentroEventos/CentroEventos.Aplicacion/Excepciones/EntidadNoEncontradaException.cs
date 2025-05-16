using System;

namespace CentroEventos.Aplicacion.Excepciones;

public class EntidadNoEncontradaException : Exception
{
    public EntidadNoEncontradaException(string mensaje) : base(mensaje) { }
}

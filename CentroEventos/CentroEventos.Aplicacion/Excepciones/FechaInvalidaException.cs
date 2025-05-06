using System;

namespace CentroEventos.Aplicacion.Excepciones;

public class FechaInvalidaException:Exception
{
        public FechaInvalidaException(string mensaje):base(mensaje){
            
        }

}

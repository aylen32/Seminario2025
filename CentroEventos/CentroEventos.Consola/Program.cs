using System;
using CentroEventos.Aplicacion;
using CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Repositorios;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Enumerativos;

class Program
{
    static void Main()
    {
        // Solicitar ID de usuario-------------------------> Importante para saber si tiene permiso o no
        Console.Write("Ingrese su ID de usuario: ");
        string? inputUsuario = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputUsuario))
        {
            Console.WriteLine("ID de usuario inválido.");
            return;
        }
        int idUsuario = int.Parse(inputUsuario);

        // Inicialización de repositorios
        IRepositorioPersona repoPersona = new RepositorioPersona();
        IRepositorioEventoDeportivo repoEvento = new RepositorioEventoDeportivo();
        IRepositorioReserva repoReserva = new RepositorioReserva();
        // Inicialización de servicio de autorizacion
        IServicioAutorizacion servicioAutorizacion = new ServicioAutorizacionProvisorio();
        // Inicialización de validadores
        IValidadorPersona validadorPersona = new ValidadorPersona(repoPersona);
        IValidadorEventoDeportivo validadorEvento = new ValidadorEventoDeportivo(repoPersona);
        IValidadorReserva validadorReserva = new ValidadorReserva(repoPersona, repoEvento, repoReserva);

        bool salir = false;//------------------------------------------------> Cuando opcion = 15 termina el loop

        while (!salir)
        {
            Console.Clear(); //----------------------------------------------> Limpia la pantalla
            Console.WriteLine("===== MENÚ DEL CENTRO DE EVENTOS =====");
            Console.WriteLine("1. Agregar a una persona nueva");
            Console.WriteLine("2. Obtener un listado de las personas");
            Console.WriteLine("3. Modificar los datos de una persona");
            Console.WriteLine("4. Eliminar a una persona");
            Console.WriteLine("5. Agregar un evento nuevo");
            Console.WriteLine("6. Obtener un listado de los eventos");
            Console.WriteLine("7. Modificar un evento");
            Console.WriteLine("8. Eliminar un evento");
            Console.WriteLine("9. Agregar una nueva reserva");
            Console.WriteLine("10. Obtener un listado de las reservas");
            Console.WriteLine("11. Modificar una reserva");
            Console.WriteLine("12. Eliminar una reserva");
            Console.WriteLine("13. Obtener un listado de la asistencia a un evento");
            Console.WriteLine("14. Obtener un listado de eventos con cupo disponible");
            Console.WriteLine("15. Salir");
            Console.Write("Seleccione una opción del menu: ");

            
            try
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        var altaPersona = new AltaPersonaUseCase(repoPersona, validadorPersona, servicioAutorizacion, idUsuario);
                        Persona p = new Persona();
                        Console.Write("Nombre: "); p.Nombre = Console.ReadLine()!;
                        Console.Write("Apellido: "); p.Apellido = Console.ReadLine()!;
                        Console.Write("DNI: "); p.DNI = Console.ReadLine()!;
                        Console.Write("Email: "); p.Mail = Console.ReadLine()!;
                        Console.Write("Teléfono: "); p.Telefono = Console.ReadLine()!;
                        altaPersona.Ejecutar(p);
                        Console.WriteLine("Persona agregada exitosamente ");
                        break;

                    case "2":
                        var listarPersonas = new ListadoPersonasUseCase(repoPersona, servicioAutorizacion, idUsuario);
                        foreach (var persona in listarPersonas.Ejecutar())
                            Console.WriteLine(persona);
                        break;

                    case "3":
                        var modificarPersona = new ModificarPersonaUseCase(repoPersona, validadorPersona, servicioAutorizacion, idUsuario);
                        Persona pMod = new Persona();
                        Console.Write("ID: "); pMod.Id = int.Parse(Console.ReadLine()!);
                        Console.Write("Nombre: "); pMod.Nombre = Console.ReadLine()!;
                        Console.Write("Apellido: "); pMod.Apellido = Console.ReadLine()!;
                        Console.Write("DNI: "); pMod.DNI = Console.ReadLine()!;
                        Console.Write("Email: "); pMod.Mail = Console.ReadLine()!;
                        Console.Write("Teléfono: "); pMod.Telefono = Console.ReadLine()!;
                        modificarPersona.Ejecutar(pMod);
                        Console.WriteLine("Persona modificada exitosamente");
                        break;

                    case "4":
                        var bajaPersona = new BajaPersonaUseCase(repoPersona, repoReserva, repoEvento, servicioAutorizacion, idUsuario);
                        Console.Write("ID de la persona a eliminar: ");
                        int idPersona = int.Parse(Console.ReadLine()!);
                        bajaPersona.Ejecutar(idPersona);
                        Console.WriteLine("Persona eliminada exitosamente");
                        break;

                    case "5":
                        var altaEvento = new AltaEventoDeportivoUseCase(repoEvento, validadorEvento, servicioAutorizacion, idUsuario);
                        EventoDeportivo e = new EventoDeportivo();
                        Console.Write("Nombre: "); e.Nombre = Console.ReadLine()!;
                        Console.Write("Descripcion: "); e.Descripcion = Console.ReadLine()!;
                        Console.Write("Fecha inicio (yyyy-MM-dd HH:mm): "); e.FechaHoraInicio = DateTime.Parse(Console.ReadLine()!);
                        Console.Write("Duración en horas: "); e.DuracionHoras = double.Parse(Console.ReadLine()!);
                        Console.Write("Cupo máximo: "); e.CupoMaximo = int.Parse(Console.ReadLine()!);
                        Console.Write("ID Responsable: "); e.ResponsableId = int.Parse(Console.ReadLine()!);
                        altaEvento.Ejecutar(e);
                        Console.WriteLine("Evento agregado exitosamente");
                        break;

                    case "6":
                        var listarEventos = new ListadoEventoDeportivoUseCase(repoEvento, servicioAutorizacion, idUsuario);
                        var eventos = listarEventos.Ejecutar();
                        if (eventos != null)
                        {
                            foreach (var evento in eventos)
                            {
                                Console.WriteLine(evento);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron eventos.");
                        }
                            
                        break;

                    case "7":
                        var modificarEvento = new ModificarEventoDeportivoUseCase(repoEvento, validadorEvento, servicioAutorizacion, idUsuario);
                        EventoDeportivo eMod = new EventoDeportivo();
                        Console.Write("ID: "); eMod.Id = int.Parse(Console.ReadLine()!);
                        Console.Write("Nombre: "); eMod.Nombre = Console.ReadLine()!;
                        Console.Write("Descripcion: "); eMod.Descripcion = Console.ReadLine()!;
                        Console.Write("Fecha inicio (yyyy-MM-dd HH:mm): "); eMod.FechaHoraInicio = DateTime.Parse(Console.ReadLine()!);
                        Console.Write("Duración en horas: "); eMod.DuracionHoras = double.Parse(Console.ReadLine()!);
                        Console.Write("Cupo máximo: "); eMod.CupoMaximo = int.Parse(Console.ReadLine()!);
                        Console.Write("ID Responsable: "); eMod.ResponsableId = int.Parse(Console.ReadLine()!);
                        modificarEvento.Ejecutar(eMod);
                        Console.WriteLine("Evento modificado exitosamente");
                        break;

                    case "8":
                        var bajaEvento = new BajaEventoDeportivoUseCase(repoEvento, repoReserva, servicioAutorizacion, idUsuario);
                        Console.Write("ID del evento a eliminar: ");
                        int idEvento = int.Parse(Console.ReadLine()!);
                        bajaEvento.Ejecutar(idEvento);
                        Console.WriteLine("Evento eliminado exitosamente");
                        break;

                    case "9":
                        var altaReserva = new AltaReservaUseCase(repoReserva, validadorReserva, repoEvento, repoPersona ,servicioAutorizacion, idUsuario);
                        Reserva r = new Reserva();
                        Console.Write("ID Persona: "); r.PersonaId = int.Parse(Console.ReadLine()!);
                        Console.Write("ID Evento: "); r.EventoDeportivoId = int.Parse(Console.ReadLine()!);
                        r.FechaAltaReserva = DateTime.Now;
                        r.Estado = Reserva.EstadoAsistencia.Pendiente;
                        altaReserva.Ejecutar(r, idUsuario);
                        Console.WriteLine("Reserva realizada exitosamente");
                        break;

                    case "10":
                        var listarReservas = new ListadoReservaUseCase(repoReserva, servicioAutorizacion, idUsuario);
                        var reservas = listarReservas.Ejecutar();
                        if (reservas != null)
                        {
                            foreach (var reserva in reservas)
                                Console.WriteLine(reserva);
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron reservas.");
                        }
                        break;

                    case "11":
                        var modificarReserva = new ModificarReservaUseCase(repoReserva, validadorReserva, servicioAutorizacion, idUsuario);
                        Reserva rMod = new Reserva();
                        Console.Write("ID: "); rMod.Id = int.Parse(Console.ReadLine()!);
                        Console.Write("ID Persona: "); rMod.PersonaId = int.Parse(Console.ReadLine()!);
                        Console.Write("ID Evento: "); rMod.EventoDeportivoId = int.Parse(Console.ReadLine()!);
                        rMod.FechaAltaReserva = DateTime.Now;
                        rMod.Estado = Reserva.EstadoAsistencia.Pendiente;
                        modificarReserva.Ejecutar(rMod);
                        Console.WriteLine("Reserva modificada exitosamente");
                        break;

                    case "12":
                        var bajaReserva = new BajaReservaUseCase(repoReserva, servicioAutorizacion, idUsuario);
                        Console.Write("ID de la reserva a eliminar: ");
                        int idReserva = int.Parse(Console.ReadLine()!);
                        bajaReserva.Ejecutar(idReserva);
                        Console.WriteLine("Reserva eliminada exitosamente");
                        break;
                        
                    case "13":
                        var listarAsistencia = new ListarAsistenciaAEventoUseCase(repoEvento, repoPersona, repoReserva, servicioAutorizacion, idUsuario);
                        Console.Write("ID del evento: ");
                        int idEv = int.Parse(Console.ReadLine()!);
                        var asistentes = listarAsistencia.Ejecutar(idEv);
                        if (asistentes != null)
                        {
                            foreach (var asistente in asistentes)
                                Console.WriteLine(asistente);
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron asistentes para este evento.");
                        }
                        break;

                    case "14":
                        var listarConCupo = new ListarEventosConCupoDisponibleUseCase(repoEvento, repoReserva, servicioAutorizacion, idUsuario);
                        foreach (var ev in listarConCupo.Ejecutar()!)
                            Console.WriteLine(ev);
                        break;

                    case "15":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción inválida. Por favor indique una nueva");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }
    }
}     
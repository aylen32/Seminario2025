using CentroEventos.UI.Components;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Repositorios;
using Microsoft.EntityFrameworkCore;
using CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Servicio;
using CentroEventos.Aplicacion.Entidades;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configurar servicios
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Registrar DbContext
builder.Services.AddDbContext<CentroEventosContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CentroEventos")));

// Repositorios
builder.Services.AddScoped<IRepositorioPersona, RepositorioPersona>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReserva>();
builder.Services.AddScoped<IRepositorioEventoDeportivo, RepositorioEventoDeportivo>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();

// Casos de uso
builder.Services.AddScoped<AltaUsuarioUseCase>();
builder.Services.AddScoped<ActualizarPermisosUsuarioUseCase>();
builder.Services.AddScoped<ListadoUsuarioUseCase>();
builder.Services.AddScoped<ModificarUsuarioUseCase>();
builder.Services.AddScoped<BajaUsuarioUseCase>();
builder.Services.AddScoped<AltaPersonaUseCase>();
builder.Services.AddScoped<BajaPersonaUseCase>();
builder.Services.AddScoped<ModificarPersonaUseCase>();
builder.Services.AddScoped<ListadoPersonasUseCase>();
builder.Services.AddScoped<ObtenerPersonaUseCase>();

builder.Services.AddScoped<AltaReservaUseCase>();
builder.Services.AddScoped<BajaReservaUseCase>();
builder.Services.AddScoped<ModificarReservaUseCase>();
builder.Services.AddScoped<ListadoReservaUseCase>();
builder.Services.AddScoped<ObtenerReservaUseCase>();

builder.Services.AddScoped<AltaEventoDeportivoUseCase>();
builder.Services.AddScoped<BajaEventoDeportivoUseCase>();
builder.Services.AddScoped<ModificarEventoDeportivoUseCase>();
builder.Services.AddScoped<ListadoEventoDeportivoUseCase>();
builder.Services.AddScoped<ObtenerEventoDeportivoUseCase>();

builder.Services.AddScoped<ListarEventosConCupoDisponibleUseCase>();
builder.Services.AddScoped<ListarAsistenciaAEventoUseCase>();


//Servicio Autorizacion y sesion
builder.Services.AddScoped<IServicioAutorizacion, ServicioAutorizacionProvisorio>();
builder.Services.AddScoped<IServicioSesion, ServicioSesion>();

//Validadores
builder.Services.AddScoped<IValidadorEventoDeportivo, ValidadorEventoDeportivo>();
builder.Services.AddScoped<IValidadorPersona, ValidadorPersona>();
builder.Services.AddScoped<IValidadorReserva, ValidadorReserva>();
builder.Services.AddScoped<IValidadorUsuario, ValidadorUsuario>();


var app = builder.Build();


//Configurar journal_mode=DELETE en SQLite
using (var scope = app.Services.CreateScope())
{
  var context = scope.ServiceProvider.GetRequiredService<CentroEventosContext>();
  //Crea la base si no existe y configura journal_mode=DELETE
  context.ConfigurarJournalModeDelete();
  context.InicializarPermisos(); //crea los permisos
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

using CentroEventos.Aplicacion;
using CentroEventos.Aplicacion.Validaciones;
using CentroEventos.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CentroEventos.Aplicacion.CasosUso;
using CentroEventos.Aplicacion.Servicio;


var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Registrar DbContext
builder.Services.AddDbContext<CentroEventosContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios
builder.Services.AddScoped<IRepositorioPersona, RepositorioPersona>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReserva>();
builder.Services.AddScoped<IRepositorioEventoDeportivo, RepositorioEventoDeportivo>();

// Casos de uso
builder.Services.AddScoped<AltaPersonaUseCase>();
builder.Services.AddScoped<BajaPersonaUseCase>();
builder.Services.AddScoped<ModificarPersonaUseCase>();
builder.Services.AddScoped<ListadoPersonasUseCase>();

builder.Services.AddScoped<AltaReservaUseCase>();
builder.Services.AddScoped<BajaReservaUseCase>();
builder.Services.AddScoped<ModificarReservaUseCase>();
builder.Services.AddScoped<ListadoReservaUseCase>();

builder.Services.AddScoped<AltaEventoDeportivoUseCase>();
builder.Services.AddScoped<BajaEventoDeportivoUseCase>();
builder.Services.AddScoped<ModificarEventoDeportivoUseCase>();
builder.Services.AddScoped<ListadoEventoDeportivoUseCase>();

builder.Services.AddScoped<ListarEventosConCupoDisponibleUseCase>();
builder.Services.AddScoped<ListarAsistenciaAEventoUseCase>();

builder.Services.AddScoped<IServicioAutorizacion, ServicioAutorizacion>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


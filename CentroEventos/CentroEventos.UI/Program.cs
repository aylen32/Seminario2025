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
builder.Services.AddScoped<AltaPersona>();
builder.Services.AddScoped<BajaPersona>();
builder.Services.AddScoped<ModificarPersona>();
builder.Services.AddScoped<ListadoPersonas>();

builder.Services.AddScoped<AltaReserva>();
builder.Services.AddScoped<BajaReserva>();
builder.Services.AddScoped<ModificarReserva>();
builder.Services.AddScoped<ListadoReservas>();

builder.Services.AddScoped<AltaEvento>();
builder.Services.AddScoped<BajaEvento>();
builder.Services.AddScoped<ModificarEvento>();
builder.Services.AddScoped<ListadoEventos>();

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


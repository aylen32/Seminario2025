using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CentroEventos.Aplicacion
{
    public class CentroEventosContextFactory_ES : IDesignTimeDbContextFactory<CentroEventosContext>
    {
        public CentroEventosContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CentroEventosContext>();
            optionsBuilder.UseSqlite("Data Source=CentroEventos.sqlite");
            return new CentroEventosContext(optionsBuilder.Options);
        }
    }
}

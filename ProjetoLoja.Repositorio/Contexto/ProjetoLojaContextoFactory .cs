using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class ProjetoLojaContextoFactory 
    : IDesignTimeDbContextFactory<ProjetoLojaContexto>
{
    public ProjetoLojaContexto CreateDbContext(string[] args)
    {
        // Lê o appsettings
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();

        // Pega a connection string
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Configura o DbContext
        var optionsBuilder = new DbContextOptionsBuilder<ProjetoLojaContexto>();

        optionsBuilder.UseSqlServer(connectionString);

        return new ProjetoLojaContexto(optionsBuilder.Options);
    }
}
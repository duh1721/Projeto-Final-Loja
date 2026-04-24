using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ProjetoLojaContextoFactory : IDesignTimeDbContextFactory<ProjetoLojaContexto>
{
    public ProjetoLojaContexto CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProjetoLojaContexto>();

        optionsBuilder.UseSqlServer("Server=NoteDuh\\SQLEXPRESS;Database=ProjetoFinalLoja;Trusted_Connection=True;TrustServerCertificate=True;");

        return new ProjetoLojaContexto(optionsBuilder.Options);
    }
}
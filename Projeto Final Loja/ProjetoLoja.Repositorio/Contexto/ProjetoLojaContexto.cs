using Microsoft.EntityFrameworkCore;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Repositorio.Configuracao;

public class ProjetoLojaContexto : DbContext
{
    private readonly DbContextOptions _options;

    public DbSet<Produtos> Produtos { get; set; }

    public ProjetoLojaContexto()
    {
    }

    public ProjetoLojaContexto(DbContextOptions options) : base(options)
    {
        _options = options;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_options == null)
        {
            optionsBuilder.UseSqlServer("Server=NoteDuh\\SQLEXPRESS;Database=ProjetoFinalLoja;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.ApplyConfiguration(new ProdutosConfiguration());
    }
}

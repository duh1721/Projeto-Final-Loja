using Microsoft.EntityFrameworkCore;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Repositorio.Configuracao;

public class ProjetoLojaContexto : DbContext
{
    public ProjetoLojaContexto(DbContextOptions<ProjetoLojaContexto> options): base(options)
    {
    }

    public DbSet<Produtos> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProdutosConfiguration());
    }
}
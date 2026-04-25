using Microsoft.EntityFrameworkCore;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Repositorio.Configuracao;

public class ProjetoLojaContexto : DbContext
{
    public ProjetoLojaContexto(DbContextOptions<ProjetoLojaContexto> options): base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Enderecos> Enderecos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItensPedido> ItensPedido { get; set; }
    public DbSet<TipoProduto> TiposProduto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProdutosConfiguration());
        modelBuilder.ApplyConfiguration(new ClientesConfiguration());
        modelBuilder.ApplyConfiguration(new EnderecoConfiguration());
        modelBuilder.ApplyConfiguration(new PedidoConfiguration());
        modelBuilder.ApplyConfiguration(new ItensPedidoConfiguration());
        modelBuilder.ApplyConfiguration(new TipoProdutoConfiguration());
    }
}
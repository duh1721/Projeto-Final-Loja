using Microsoft.EntityFrameworkCore;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Repositorio.Configuracao;

public class ProjetoLojaContexto : DbContext
{
    public ProjetoLojaContexto(DbContextOptions<ProjetoLojaContexto> options)
        : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }

    public DbSet<Clientes> Clientes { get; set; }

    public DbSet<Endereco> Enderecos { get; set; }

    public DbSet<Pedido> Pedidos { get; set; }

    public DbSet<ItensPedido> ItensPedido { get; set; }

    public DbSet<TipoProduto> TiposProduto { get; set; }

    public DbSet<Login> Logins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProdutosConfiguration());

        modelBuilder.ApplyConfiguration(new ClientesConfiguration());

        modelBuilder.ApplyConfiguration(new EnderecoConfiguration());

        modelBuilder.ApplyConfiguration(new PedidoConfiguration());

        modelBuilder.ApplyConfiguration(new ItensPedidoConfiguration());

        modelBuilder.ApplyConfiguration(new TipoProdutoConfiguration());


        modelBuilder.Entity<Clientes>()
            .HasOne(c => c.Login)
            .WithOne(l => l.Cliente)
            .HasForeignKey<Login>(l => l.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
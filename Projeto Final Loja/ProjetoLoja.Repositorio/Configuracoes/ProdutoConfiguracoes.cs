using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoLoja.Dominio.Entidades;


namespace ProjetoLoja.Repositorio.Configuracao
{
    public class ProdutosConfiguration : IEntityTypeConfiguration<Produtos>
    {
        public void Configure(EntityTypeBuilder<Produtos> builder)
        {
            builder.ToTable("Produtos").HasKey(p => p.Id);

            builder.Property(nameof(Produtos.Id)).HasColumnName("ProdutoId");
            builder.Property(nameof(Produtos.Nome)).HasColumnName("Nome").IsRequired(true);
            builder.Property(nameof(Produtos.Preco)).HasColumnName("Preco").IsRequired(true);
            builder.Property(nameof(Produtos.Quantidade)).HasColumnName("Quantidade").IsRequired(true);
            
        }
    }
}
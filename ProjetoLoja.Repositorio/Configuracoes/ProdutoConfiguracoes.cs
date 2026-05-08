using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoLoja.Dominio.Entidades;


namespace ProjetoLoja.Repositorio.Configuracao
{
    public class ProdutosConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos").HasKey(p => p.Id);

            builder.Property(nameof(Produto.Id)).HasColumnName("ProdutoId");
            builder.Property(nameof(Produto.Nome)).HasColumnName("Nome").IsRequired(true);
            builder.Property(nameof(Produto.Preco)).HasPrecision(18, 2).HasColumnName("Preco").IsRequired(true);
            builder.Property(nameof(Produto.Quantidade)).HasColumnName("Quantidade").IsRequired(true);
            builder.Property(nameof(Produto.Descricao)).HasColumnName("Descricao").IsRequired(false);
            builder.Property(nameof(Produto.Ativo)).HasColumnName("Ativo").IsRequired(true);
            builder.Property(nameof(Produto.TipoProdutoId)).HasColumnName("TipoProdutoId").IsRequired(true);

            builder.HasOne(t => t.TipoProduto).WithMany(s => s.Produto).HasForeignKey(t => t.TipoProdutoId);

        }
    }
}
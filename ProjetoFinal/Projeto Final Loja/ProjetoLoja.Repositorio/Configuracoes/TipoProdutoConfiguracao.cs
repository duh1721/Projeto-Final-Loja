using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Configuracao
{
    public class TipoProdutoConfiguration : IEntityTypeConfiguration<TipoProduto>
    {
        public void Configure(EntityTypeBuilder<TipoProduto> builder)
        {
            builder.ToTable("TipoProduto").HasKey(tp => tp.Id);

            builder.Property(nameof(TipoProduto.Id)).HasColumnName("TipoProdutoId");
            builder.Property(nameof(TipoProduto.Nome)).HasColumnName("Nome").IsRequired(true);
            builder.Property(nameof(TipoProduto.Ativo)).HasColumnName("Ativo").IsRequired(true);
        }
    }
}
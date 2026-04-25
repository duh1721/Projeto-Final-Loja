using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Configuracao
{
    public class ItensPedidoConfiguration : IEntityTypeConfiguration<ItensPedido>
    {
        public void Configure(EntityTypeBuilder<ItensPedido> builder)
        {
            builder.ToTable("ItensPedido").HasKey(i => i.Id);

            builder.Property(nameof(ItensPedido.Id)).HasColumnName("ItensPedidoId");
            builder.Property(nameof(ItensPedido.PedidoId)).HasColumnName("PedidoId").IsRequired(true);
            builder.Property(nameof(ItensPedido.ProdutoId)).HasColumnName("ProdutoId").IsRequired(true);
            builder.Property(nameof(ItensPedido.Quantidade)).HasColumnName("Quantidade").IsRequired(true);
            builder.Property(nameof(ItensPedido.PrecoUnitario)).HasColumnName("PrecoUnitario").IsRequired(true);
            builder.Property(nameof(ItensPedido.Ativo)).HasColumnName("Ativo").IsRequired(true);

            builder.HasOne(p => p.Pedido).WithMany(i => i.ItensPedido).HasForeignKey(p => p.PedidoId);
            builder.HasOne(p => p.Produto).WithMany().HasForeignKey(p => p.ProdutoId);
        }
    }
}
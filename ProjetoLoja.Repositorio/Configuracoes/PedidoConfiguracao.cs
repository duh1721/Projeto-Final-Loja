using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Configuracao
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos").HasKey(p => p.Id);

            builder.Property(nameof(Pedido.Id)).HasColumnName("PedidoId");
            builder.Property(nameof(Pedido.ClienteId)).HasColumnName("ClienteId").IsRequired(true);
            builder.Property(nameof(Pedido.EnderecoId)).HasColumnName("EnderecoId").IsRequired(true);
            builder.Property(nameof(Pedido.ValorTotal)).HasColumnName("ValorTotal").IsRequired(true);
            builder.Property(nameof(Pedido.DataPedido)).HasColumnName("Data").IsRequired(true);
            builder.Property(nameof(Pedido.Ativo)).HasColumnName("Ativo").IsRequired(true);

                builder.HasOne(c => c.Cliente).WithMany(p => p.Pedidos).HasForeignKey(c => c.ClienteId);
                builder.HasOne(e => e.Endereco).WithMany(p => p.Pedidos).HasForeignKey(e => e.EnderecoId);
        }
    }
}
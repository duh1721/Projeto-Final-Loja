using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Configuracao
{
    public class ClientesConfiguration : IEntityTypeConfiguration<Clientes>
    {
        public void Configure(EntityTypeBuilder<Clientes> builder)
        {
            builder.ToTable("Clientes").HasKey(c => c.Id);

            builder.Property(nameof(Clientes.Id)).HasColumnName("ClientId");
            builder.Property(nameof(Clientes.Nome)).HasColumnName("Nome").IsRequired(true);
            builder.Property(nameof(Clientes.Email)).HasColumnName("Email").IsRequired(true);
            builder.Property(nameof(Clientes.Telefone)).HasColumnName("Telefone").IsRequired(true);
            builder.Property(nameof(Clientes.Ativo)).HasColumnName("Ativo").IsRequired(true);
        }
    }
}
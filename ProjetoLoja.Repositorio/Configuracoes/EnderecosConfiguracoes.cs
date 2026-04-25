using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Configuracao
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Enderecos>
    {
        public void Configure(EntityTypeBuilder<Enderecos> builder)
        {
            builder.ToTable("Enderecos").HasKey(e => e.Id);

            builder.Property(nameof(Enderecos.Id)).HasColumnName("EnderecoId");
            builder.Property(nameof(Enderecos.Rua)).HasColumnName("Rua").IsRequired(true);
            builder.Property(nameof(Enderecos.Numero)).HasColumnName("Numero").IsRequired(true);
            builder.Property(nameof(Enderecos.Bairro)).HasColumnName("Bairro").IsRequired(true);
            builder.Property(nameof(Enderecos.Cidade)).HasColumnName("Cidade").IsRequired(true);
            builder.Property(nameof(Enderecos.Estado)).HasColumnName("Estado").IsRequired(true);
            builder.Property(nameof(Enderecos.Cep)).HasColumnName("Cep").IsRequired(true);
            builder.Property(nameof(Enderecos.Ativo)).HasColumnName("Ativo").IsRequired(true);
        }
    }
}
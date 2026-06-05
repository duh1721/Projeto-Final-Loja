using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Configuracao
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Enderecos").HasKey(e => e.Id);

            builder.Property(nameof(Endereco.Id)).HasColumnName("EnderecoId");
            builder.Property(nameof(Endereco.Rua)).HasColumnName("Rua").IsRequired(true);
            builder.Property(nameof(Endereco.Numero)).HasColumnName("Numero").IsRequired(true);
            builder.Property(nameof(Endereco.Bairro)).HasColumnName("Bairro").IsRequired(true);
            builder.Property(nameof(Endereco.Cidade)).HasColumnName("Cidade").IsRequired(true);
            builder.Property(nameof(Endereco.Estado)).HasColumnName("Estado").IsRequired(true);
            builder.Property(nameof(Endereco.Cep)).HasColumnName("Cep").IsRequired(true);
            builder.Property(nameof(Endereco.Ativo)).HasColumnName("Ativo").IsRequired(true);
        }
    }
}
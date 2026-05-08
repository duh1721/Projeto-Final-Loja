using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Configuracao
{
    public class LoginConfiguracoes : IEntityTypeConfiguration<Login>
    {
        public void Configure(EntityTypeBuilder<Login> builder)
        {
            builder.ToTable("Login").HasKey(c => c.Email);

            builder.Property(nameof(Login.Email)).HasColumnName("Email").IsRequired(true);
            builder.Property(nameof(Login.Senha)).HasColumnName("Senha").IsRequired(true);
        }
    }
}
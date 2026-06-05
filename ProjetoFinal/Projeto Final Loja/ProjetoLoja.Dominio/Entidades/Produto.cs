using System.ComponentModel;

namespace ProjetoLoja.Dominio.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

        public bool Ativo { get; set; }

        public int TipoProdutoId { get; set; }
        public TipoProduto TipoProduto { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public Produto()
        {
            Ativo = true;
        }

        public void Deletar()
        {
            Ativo = false;
        }

        public void Restaurar()
        {
            Ativo = true;
        }
    }
}

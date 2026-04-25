using System.ComponentModel;

namespace ProjetoLoja.Dominio.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public required int TipoProdutoId { get; set; }
        public TipoProduto TipoProduto { get; set; }
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();


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
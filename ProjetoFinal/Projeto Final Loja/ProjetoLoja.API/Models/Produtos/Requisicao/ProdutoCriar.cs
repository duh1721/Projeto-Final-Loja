namespace ProjetoLoja.API.Models.Requisicao
{
    public class ProdutoCriar
    {
        public required string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public required string Descricao { get; set; }
        public bool Ativo { get; set; }
        public int TipoProdutoId { get; set; }
    }
}
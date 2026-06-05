namespace ProjetoLoja.API.Models.TipoProduto.Resposta
{
    public class TipoProdutoResposta
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
namespace ProjetoLoja.API.Models.Resposta
{
    public class ProdutoAtualizar
    {
        public required string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public required string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
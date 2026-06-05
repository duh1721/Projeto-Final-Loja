namespace ProjetoLoja.API.Models.ItensPedido.Resposta
{
    public class ItensPedidoResposta
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
namespace ProjetoLoja.API.Models.ItensPedido.Requisicao
{
    public class ItensPedidoAtualizar
    {
        public required int Id { get; set; }
        public required int PedidoId { get; set; }
        public required int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
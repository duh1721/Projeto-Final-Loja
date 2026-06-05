namespace ProjetoLoja.API.Models.Pedidos.Requisicao
{
    public class PedidoAtualizar
    {
        public required int Id { get; set; }
        public required int ClienteId { get; set; }
        public required int EnderecoId { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public int ItensPedidoId { get; set; }
        public bool Ativo { get; set; }
    }
}
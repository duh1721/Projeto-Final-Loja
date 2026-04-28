namespace ProjetoLoja.API.Models.Pedidos.Resposta
{
    public class PedidoResposta
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int EnderecoId { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        
    }
}
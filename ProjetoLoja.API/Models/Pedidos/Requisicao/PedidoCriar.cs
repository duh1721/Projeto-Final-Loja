namespace ProjetoLoja.API.Models.Pedidos.Requisicao
{
    public class PedidoCriar
    {
        public required int ClienteId { get; set; }
        public required int EnderecoId { get; set; }
    }
}
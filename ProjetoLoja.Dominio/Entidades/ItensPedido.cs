namespace ProjetoLoja.Dominio.Entidades
{
    public class ItensPedido
    {
        public int Id { get; set; }
        public required int PedidoId { get; set; }
        public required Pedido Pedido { get; set; }
        public required int ProdutoId { get; set; }
        public required Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public bool Ativo { get; set; }

        public ItensPedido()
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
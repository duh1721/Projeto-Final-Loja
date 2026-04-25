namespace ProjetoLoja.Dominio.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public required int ClienteId { get; set; }
        public required Clientes Cliente { get; set; }
        public required int EnderecoId { get; set; }
        public required Enderecos Endereco { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public bool Ativo { get; set; }

        public ICollection<ItensPedido> ItensPedido { get; set; } = new List<ItensPedido>();


        public Pedido()
        {
            Ativo = true;
            DataPedido = DateTime.Now;
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